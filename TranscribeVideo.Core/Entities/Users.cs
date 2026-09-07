using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsEmailVerified { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Roles Role { get; set; } = null!;

        public ICollection<RefreshTokens> RefreshTokens { get; set; } = new List<RefreshTokens>();

        public ICollection<Videos> Videos { get; set; } = new List<Videos>();
    }
}
