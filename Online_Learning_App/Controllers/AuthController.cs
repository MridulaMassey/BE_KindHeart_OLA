using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            bool isAuthenticated = await _authService.AuthenticateUserAsync(model);
            if (!isAuthenticated)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { message = "Login successful" });
        }
    }
}
