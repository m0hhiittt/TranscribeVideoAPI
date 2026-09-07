using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.ProcessJob
{
    public class ProcessingJobResponseDto
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public string Status { get; set; } = string.Empty;

        public int RetryCount { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
