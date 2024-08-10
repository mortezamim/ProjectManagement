using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// AllowAnonymous
        /// </summary>
        [HttpGet("Landing")]
        public async Task<IActionResult> LandingVisited()
        {
            return Ok("Landing");
        }

        /// <summary>
        /// AllowAnonymous
        /// </summary>
        [HttpGet("Register")]
        public async Task<IActionResult> RegisterVisited()
        {
            return Ok("Register");
        }

    }
}
