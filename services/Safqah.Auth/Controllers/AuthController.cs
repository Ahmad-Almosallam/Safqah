using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Safqah.Auth.Models;
using Safqah.Shared.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Safqah.Auth.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IConfiguration configuration,
                              UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("connect/token")]
        public async Task<ActionResult<TokenModel>> Login(LoginModel loginModel)
        {

            var user = await _userManager.FindByNameAsync(loginModel.PhoneNumber);
            if (user != null && loginModel.OTP == "1234")
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                var token = JWTHelper.GetToken(authClaims, _configuration);

                return Ok(new TokenModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }

            return BadRequest();
        }
    }
}
