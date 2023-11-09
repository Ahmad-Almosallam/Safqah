using MassTransit;
using Safqah.Opportunities.Data;
using Safqah.Opportunities.Entities;
using Safqah.Shared.BaseRepository;
using Safqah.Shared.MessagesContracts;
using System;
using System.Threading.Tasks;

namespace Safqah.Opportunities.Consumer.Consumers
{
    public class InvestSubmittedConsumer : IConsumer<InvestSubmitted>
    {
        private readonly IRepository<Opportunity, long, OpportunityDbContext> _opportunityRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public InvestSubmittedConsumer(
            IRepository<Opportunity, long, OpportunityDbContext> opportunityRepository,
            IPublishEndpoint publishEndpoint)
        {
            _opportunityRepository = opportunityRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<InvestSubmitted> context)
        {
            var message = context.Message;
            var opportunity = await _opportunityRepository.GetByAsync(x => x.Id == message.OpportunityId);

            // Check if the opportunity is not completed and that the amount to invest + InvestedAmount is not larger than the TotalAmount
            if (opportunity != null && opportunity.IsCompleted == false && opportunity.InvestedAmount + message.Amount <= opportunity.TotalAmount)
            {
                opportunity.InvestedAmount += message.Amount;
                if (opportunity.InvestedAmount == opportunity.TotalAmount)
                {
                    opportunity.IsCompleted = true;
                    opportunity.CompletionDate = DateTime.Now;
                }
                await _opportunityRepository.UpdateAsync(opportunity);
                // if all is done deduct from the wallet the amount
                await _publishEndpoint.Publish(new InvestDone(message.UserId, message.Amount));
            }
            else
            {
                // TODO: Send message that the opportunity is closed and the invesment is not done
            }
        }
    }
}
