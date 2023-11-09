namespace Safqah.Shared.MessagesContracts
{
    public class InvestDone
    {
        public InvestDone(string userId, decimal amount)
        {
            UserId = userId;
            Amount = amount;
        }

        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
