using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Requests;

namespace TaskTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}