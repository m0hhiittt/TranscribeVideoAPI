using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Video
{
    public class UpdateVideoDto
    {
        public int Id { get; set; }

        public string? Language { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
