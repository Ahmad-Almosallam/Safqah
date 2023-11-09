namespace Safqah.Opportunities.Models
{
    public class CreateOpportunityModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InvestedAmount { get; set; }
    }
}
