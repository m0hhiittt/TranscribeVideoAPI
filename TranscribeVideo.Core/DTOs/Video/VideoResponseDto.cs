using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Transcript;

namespace TranscribeVideo.Core.DTOs.Video
{
    public class VideoResponseDto
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public int? DurationInSeconds { get; set; }

        public string? Language { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }

        public VideoResponseDto Video { get; set; } = null!;

        public TranscriptResponseDto? Transcript { get; set; }
    }
}
