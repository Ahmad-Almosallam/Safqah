using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Safqah.Investors.Entities;
using Safqah.Investors.Models;
using System.Threading.Tasks;

namespace Safqah.Investors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvestorController : ControllerBase
    {
        private readonly UserManager<Investor> _userManager;

        public InvestorController(UserManager<Investor> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> Register(RegisterModel registerModel)
        {
            var user = await _userManager.FindByNameAsync(registerModel.PhoneNumber);
            if (user == null && registerModel.OTP == "1234")
            {
                var res = await _userManager.CreateAsync(new Investor()
                {
                    PhoneNumber = registerModel.PhoneNumber,
                    UserName = registerModel.PhoneNumber
                });
                if (res.Succeeded)
                    return Ok(res);

                return BadRequest(res);
            }

            return BadRequest();
        }


        [HttpGet("{investorId}")]
        public async Task<ActionResult<Investor>> Get(string investorId)
        {
            var investor = await _userManager.FindByIdAsync(investorId);
            return Ok(investor);
        }

        [HttpPut("{investorId}")]
        public async Task<ActionResult<IdentityResult>> Update([FromBody] UpdateInvestorModel investorModel, [FromRoute] string investorId)
        {
            var investor = await _userManager.FindByIdAsync(investorId);
            if(investor != null && investorModel.OTP == "1234")
            {
                investor.PhoneNumber = investorModel.PhoneNumber;
                investor.UserName = investorModel.PhoneNumber;
                var res = await _userManager.UpdateAsync(investor);

                if (res.Succeeded)
                    return Ok(res);

                return BadRequest(res);
            }
            return BadRequest();
        }

        [HttpGet("{investorId}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(string investorId)
        {
            var investor = await _userManager.FindByIdAsync(investorId);
            return Ok(investor.Balance);
        }
    }
}
