using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Safqah.Opportunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
