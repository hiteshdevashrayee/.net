using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_I.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet ("test")]
        public IActionResult Test() 
        { 
            return Ok(new {test = true});
        }
    }
}
