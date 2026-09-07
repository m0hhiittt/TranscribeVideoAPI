using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("RefreshTokens")]
    public class RefreshTokens
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public Users User { get; set; } = null!;
    }
}
