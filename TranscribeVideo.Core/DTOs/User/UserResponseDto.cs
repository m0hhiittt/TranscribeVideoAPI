using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public string RoleName { get; set; } = string.Empty;
    }
}
