using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using CardiacPatientMonitoringSystem.API.DTOs.Responses;
using CardiacPatientMonitoringSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.API.Controllers
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

        /// <summary>
        /// Registers a new patient account.
        /// </summary>
        /// <param name="request">
        /// The registration information for the new patient.
        /// </param>
        /// <response code="201">
        /// The patient account was created successfully.
        /// </response>
        /// <response code="400">
        /// The registration request is invalid or the email already exists.
        /// </response>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT access token.
        /// </summary>
        /// <param name="request">
        /// The user's email and password.
        /// </param>
        /// <response code="200">
        /// Login completed successfully and a JWT token was returned.
        /// </response>
        /// <response code="401">
        /// The email or password is invalid.
        /// </response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}