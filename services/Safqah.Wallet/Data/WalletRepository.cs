using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Safqah.Wallet.Data
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _walletDbContext;
        public WalletRepository(WalletDbContext dbContext)
        {
            _walletDbContext = dbContext;
        }

        public async Task Invest(string userId, decimal amount)
        {
            await _walletDbContext.Database.ExecuteSqlRawAsync(@"UPDATE 
                                                              ""AspNetUsers"" 
                                                            SET 
                                                              ""Balance"" = ""Balance"" - {0}
                                                            WHERe 
                                                              ""Id"" = {1};", amount, userId);
        }

        public async Task TopupWallet(string userId, decimal amount)
        {
            await _walletDbContext.Database.ExecuteSqlRawAsync(@"UPDATE 
                                                              ""AspNetUsers"" 
                                                            SET 
                                                              ""Balance"" = ""Balance"" + {0}
                                                            WHERe 
                                                              ""Id"" = {1};", amount, userId);
        }
    }
}
