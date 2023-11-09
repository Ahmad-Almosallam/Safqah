using MassTransit;
using MassTransit.RabbitMqTransport.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqah.Opportunities.Data;
using Safqah.Opportunities.Entities;
using Safqah.Opportunities.HttpClients;
using Safqah.Opportunities.Models;
using Safqah.Shared.BaseClases;
using Safqah.Shared.BaseRepository;
using Safqah.Shared.MessagesContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safqah.Opportunities.Controllers
{

    [Authorize]
    public class OpportunityController : BaseController
    {
        private readonly IRepository<Opportunity, long, OpportunityDbContext> _opportunityRepository;
        private readonly IInvestorClient _investorClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public OpportunityController(
            IRepository<Opportunity, long, OpportunityDbContext> opportunityRepository,
            IInvestorClient investorClient,
            IPublishEndpoint publishEndpoint)
        {
            _opportunityRepository = opportunityRepository;
            _investorClient = investorClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Opportunity>>> GetAll()
        {
            var list = await _opportunityRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Opportunity>> Create(CreateOpportunityModel opportunityModel)
        {
            var opportunity = new Opportunity()
            {
                Title = opportunityModel.Title,
                Description = opportunityModel.Description,
                TotalAmount = opportunityModel.TotalAmount,
                InvestedAmount = 0,
                CreatedAt = DateTime.Now,
                CreatorId = _userId
            };
            var res = await _opportunityRepository.AddAsync(opportunity);
            return Ok(res);
        }

        [HttpPost("invest")]
        public async Task<ActionResult> Invest(InvestModel investModel)
        {
            // 1- Check if User Can Invest based on the wallet balance

            var balance = await _investorClient.GetBalance(_userId);
            if (balance <= 0) return BadRequest("Balance is less than 0");

            // TODO: Add to the queue of Investment
            await _publishEndpoint.Publish(new InvestSubmitted(_userId, investModel.OpportunityId, investModel.Amount));
            
            return Ok();
        }
    }
}
