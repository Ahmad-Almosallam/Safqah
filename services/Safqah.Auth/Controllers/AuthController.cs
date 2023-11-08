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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<TokenModel>> Login()
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "UserName"),
                    new Claim(ClaimTypes.NameIdentifier, "UserId"),
                };

            var token = JWTHelper.GetToken(authClaims, _configuration);

            return Ok(new TokenModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }
    }
}
