using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Auth;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var user = new Users
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId,
                IsActive = true,
                IsEmailVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            user = await _userRepository.GetByEmailAsync(request.Email);

            var accessToken = _jwtService.GenerateAccessToken(user!);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshTokens
            {
                UserId = user!.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!isValidPassword)
                throw new Exception("Invalid email or password.");

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshTokens
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var storedToken =
                await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (storedToken == null ||
                storedToken.IsRevoked ||
                storedToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new Exception("Invalid refresh token.");
            }

            var user = await _userRepository.GetUserWithRoleAsync(storedToken.UserId);

            var newAccessToken = _jwtService.GenerateAccessToken(user!);

            return new AuthResponseDto
            {
                UserId = user!.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = newAccessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken =
                await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (storedToken == null)
                return false;

            storedToken.IsRevoked = true;

            await _refreshTokenRepository.Update(storedToken, storedToken.Id);

            return true;
        }
    }
}
