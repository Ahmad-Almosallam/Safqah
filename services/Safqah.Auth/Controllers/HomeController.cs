using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Safqah.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
