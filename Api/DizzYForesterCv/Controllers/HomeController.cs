using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("getHome")]
        [JwtAuth]
        public ActionResult GetHome()
        {
            return Ok();
        }
    }
}
