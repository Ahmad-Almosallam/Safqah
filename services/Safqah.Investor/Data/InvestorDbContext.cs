using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Safqah.Investors.Entities;
using System.Reflection;

namespace Safqah.Investors.Data
{
    public class InvestorDbContext : IdentityDbContext
    {

        public InvestorDbContext(DbContextOptions<InvestorDbContext> options) : base(options)
        {
        }


        public DbSet<Investor> Investors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
