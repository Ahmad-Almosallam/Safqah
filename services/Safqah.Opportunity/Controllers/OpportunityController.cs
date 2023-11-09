using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqah.Opportunities.Data;
using Safqah.Opportunities.Entities;
using Safqah.Opportunities.Models;
using Safqah.Shared.BaseClases;
using Safqah.Shared.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Safqah.Opportunities.Controllers
{

    [Authorize]
    public class OpportunityController : BaseController
    {
        private readonly IRepository<Opportunity, long, OpportunityDbContext> _opportunityRepository;

        public OpportunityController(IRepository<Opportunity, long, OpportunityDbContext> opportunityRepository)
        {
            _opportunityRepository = opportunityRepository;
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
                Title= opportunityModel.Title,
                Description= opportunityModel.Description,
                TotalAmount= opportunityModel.TotalAmount,
                InvestedAmount = 0,
                CreatedAt = DateTime.Now,
                CreatorId = _userId
            };
            var res = await _opportunityRepository.AddAsync(opportunity);
            return Ok(res);
        }

        [HttpPost("{opportunityId}/invest")]
        public async Task<ActionResult> Invest(long opportunityId)
        {
            // TODO: Add to the queue of Investment, opportunityId + userId
            return Ok();
        }
    }
}
