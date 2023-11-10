using Microsoft.EntityFrameworkCore;
using Safqah.Opportunities.Entities;

namespace Safqah.Opportunities.Data
{
    public class OpportunityDbContext : DbContext
    {

        public OpportunityDbContext(DbContextOptions<OpportunityDbContext> options) : base(options)
        {
        }

        public DbSet<Opportunity> Opportunities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
