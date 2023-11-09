using Microsoft.EntityFrameworkCore;

namespace Safqah.Wallet.Data
{
    public class WalletDbContext : DbContext
    {

        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
