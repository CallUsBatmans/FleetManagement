using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheFleet.Services.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [Route("secret")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Secret message from FleetServices");
        }
    }
}