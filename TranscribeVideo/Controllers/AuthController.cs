using Microsoft.AspNetCore.Mvc;
using TranscribeVideo.Core.DTOs.Auth;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);

            return Ok(result);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(string refreshToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(refreshToken);

            return Ok(result);
        }
    }
}
