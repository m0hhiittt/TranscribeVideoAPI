using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Summary
{
    public class SummaryResponseDto
    {
        public int Id { get; set; }

        public int TranscriptId { get; set; }

        public string SummaryText { get; set; } = string.Empty;

        public string? GeneratedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
