using BCrypt.Net;
using TranscribeVideo.Core.DTOs.User;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Mappers
{
    public static class UserMapper
    {
        public static Users ToEntity(CreateUserDto dto)
        {
            return new Users
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
                IsActive = true,
                IsEmailVerified = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UserResponseDto ToDto(Users user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                IsActive = user.IsActive,
                IsEmailVerified = user.IsEmailVerified,
                LastLoginAt = user.LastLoginAt,
                RoleName = user.Role?.Name ?? string.Empty
            };
        }

        public static void UpdateEntity(Users user, UpdateUserDto dto)
        {
            user.FullName = dto.FullName;
            user.ProfileImageUrl = dto.ProfileImageUrl;
            user.RoleId = dto.RoleId;
            user.IsActive = dto.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
        }
    }
}
