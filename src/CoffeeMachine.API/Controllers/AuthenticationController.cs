using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
    
        public AuthenticationController()
        {
            
        }

        [Authorize]
        [HttpGet("/")]
        public async Task<IActionResult> Authorization()
        {
            return Ok("Hello World!");
        }
    }
}