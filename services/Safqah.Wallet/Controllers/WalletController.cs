using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqah.Shared.BaseClases;
using Safqah.Wallet.Data;
using Safqah.Wallet.Models;
using System.Threading.Tasks;

namespace Safqah.Wallet.Controllers
{
    [Authorize]
    public class WalletController : BaseController
    {

        private readonly IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpPost("topup/{userId}/{amount}")]
        public async Task<ActionResult> TopupWallet(string userId, decimal amount)
        {
            await _walletRepository.TopupWallet(userId, amount);
            return Ok();
        }


        [HttpPost("invest")]
        public async Task<ActionResult> Invest(InvestModel investModel)
        {
            await _walletRepository.Invest(investModel.UserId, investModel.Amount);
            // TODO: Add To queue of Opportunity to sum the InvestedAmount
            return Ok();
        }
    }
}
