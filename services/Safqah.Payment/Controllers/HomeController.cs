﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Safqah.Payment.Controllers
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