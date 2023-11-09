using System.Threading.Tasks;

namespace Safqah.Wallet.Data
{
    public interface IWalletRepository
    {
        Task TopupWallet(string userId, decimal amount);
        Task Invest(string userId, decimal amount);
    }
}
