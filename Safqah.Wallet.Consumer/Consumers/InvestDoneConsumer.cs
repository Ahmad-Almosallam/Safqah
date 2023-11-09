using MassTransit;
using Safqah.Shared.MessagesContracts;
using Safqah.Wallet.Data;
using System.Threading.Tasks;

namespace Safqah.Wallet.Consumer.Consumers
{
    public class InvestDoneConsumer : IConsumer<InvestDone>
    {
        private readonly IWalletRepository _walletRepository;

        public InvestDoneConsumer(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task Consume(ConsumeContext<InvestDone> context)
        {
            await _walletRepository.Invest(context.Message.UserId, context.Message.Amount);
        }
    }
}
