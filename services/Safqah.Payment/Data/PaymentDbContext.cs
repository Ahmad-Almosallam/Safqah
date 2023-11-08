using Microsoft.EntityFrameworkCore;
using Safqah.Payment.Entites;

namespace Safqah.Payment.Data
{
    public class PaymentDbContext : DbContext
    {

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
