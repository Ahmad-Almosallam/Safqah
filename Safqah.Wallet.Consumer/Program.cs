using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Safqah.Wallet.Consumer.Consumers;
using Safqah.Wallet.Data;

namespace Safqah.Wallet.Consumer
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
                    
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PaymentSucceedConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddDbContext<WalletDbContext>(options =>
                    {
                        options.UseNpgsql(hostContext.Configuration.GetConnectionString("Default"));
                    });
                    services.AddScoped(typeof(IWalletRepository), typeof(WalletRepository));
                    services.AddHostedService<Worker>();
                });
    }
}
