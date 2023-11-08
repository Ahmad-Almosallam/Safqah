using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Safqah.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SafqahDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
            });

        }
    }
}
