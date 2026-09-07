using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.User
{
    public class UpdateUserDto
    {
        public string FullName { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
    }
}
