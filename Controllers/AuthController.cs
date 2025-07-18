using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VisionOfChosen_BE.DTOs.Auth;
using VisionOfChosen_BE.Services;

namespace VisionOfChosen_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var token = _authService.Authenticate(request.Email, request.Password);
            if (token == null)
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var success = await _authService.RegisterAsync(request);
            if (!success)
                return BadRequest("Email Đã tồn tại");

            return Ok("Register success");
        }
    }
}
