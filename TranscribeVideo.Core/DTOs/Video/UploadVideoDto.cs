using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Video
{
    public class UploadVideoDto
    {
        public IFormFile VideoFile { get; set; } = null!;

        public string? Language { get; set; }
    }
}
