namespace Safqah.Shared.MessagesContracts
{
    public class InvestSubmitted
    {
        public InvestSubmitted(string userId, long opportunityId, decimal amount)
        {
            UserId = userId;
            OpportunityId = opportunityId;
            Amount = amount;
        }

        public string UserId { get; set; }
        public long OpportunityId { get; set; }
        public decimal Amount { get; set; }
    }
}
