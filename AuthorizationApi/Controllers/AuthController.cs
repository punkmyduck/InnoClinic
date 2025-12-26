using Microsoft.AspNetCore.Mvc;

namespace AuthorizationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            
        }

        [HttpPut]
        public async Task<IActionResult> Register()
        {
            return Ok(new { smth = "smth" });
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return Ok(new { smth = "smth" });
        }
    }
}
