using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Safqah.Opportunities.Consumer.Consumers;
using Safqah.Opportunities.Data;
using Safqah.Shared.BaseRepository;

namespace Safqah.Opportunities.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<OpportunityDbContext>(options =>
                    {
                        options.UseNpgsql(hostContext.Configuration.GetConnectionString("Default"));
                    });
                    services.AddScoped(typeof(IRepository<,,>), typeof(Repository<,,>));
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<InvestSubmittedConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
