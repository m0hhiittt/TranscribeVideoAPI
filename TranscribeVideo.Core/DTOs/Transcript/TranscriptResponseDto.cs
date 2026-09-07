using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Transcript
{
    public class TranscriptResponseDto
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public string TranscriptText { get; set; } = string.Empty;

        public decimal? ConfidenceScore { get; set; }

        public int? ProcessingTimeInSeconds { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
