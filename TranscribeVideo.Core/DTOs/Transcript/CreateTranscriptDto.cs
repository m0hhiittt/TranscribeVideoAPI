using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Transcript
{
    public class CreateTranscriptDto
    {
        public int VideoId { get; set; }

        public string TranscriptText { get; set; } = string.Empty;

        public decimal? ConfidenceScore { get; set; }

        public int? ProcessingTimeInSeconds { get; set; }
    }
}
