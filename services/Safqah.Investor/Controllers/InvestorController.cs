using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Safqah.Investors.Entities;
using Safqah.Investors.Models;
using System.Threading.Tasks;

namespace Safqah.Investors.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class InvestorController : ControllerBase
    {
        private readonly UserManager<Investor> _userManager;

        public InvestorController(UserManager<Investor> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel registerModel)
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


        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
