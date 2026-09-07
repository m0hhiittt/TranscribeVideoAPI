using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Auth;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}
