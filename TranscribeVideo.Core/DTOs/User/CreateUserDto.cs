using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.User
{
    public class CreateUserDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}
