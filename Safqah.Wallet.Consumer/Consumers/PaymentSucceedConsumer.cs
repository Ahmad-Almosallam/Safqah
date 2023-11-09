using MassTransit;
using Safqah.Shared.MessagesContracts;
using Safqah.Wallet.Data;
using System.Threading.Tasks;

namespace Safqah.Wallet.Consumer.Consumers
{
    public class PaymentSucceedConsumer : IConsumer<PaymentSucceed>
    {
        private readonly IWalletRepository _walletRepository;

        public PaymentSucceedConsumer(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task Consume(ConsumeContext<PaymentSucceed> context)
        {
            var message = context.Message;
            await _walletRepository.TopupWallet(message.UserId, message.Amount);
        }
    }
}
