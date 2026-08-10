using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Requests;
using TaskTrackerApi.Services.Interfaces;

namespace TaskTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(new{token});
        }
    }
}