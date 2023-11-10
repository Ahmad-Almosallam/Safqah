using System.Threading.Tasks;

namespace Safqah.Opportunities.HttpClients
{
    public interface IInvestorClient
    {
        Task<decimal> GetBalance(string ivestorId);
    }
}
