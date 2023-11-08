using Microsoft.AspNetCore.Identity;

namespace Safqah.Investors.Entities
{
    public class Investor : IdentityUser
    {
        public decimal Balance { get; set; }
    }
}
