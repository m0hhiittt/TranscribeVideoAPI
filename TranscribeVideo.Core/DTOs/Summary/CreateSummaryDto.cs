using System;
using System.Collections.Generic;
using System.Text;

namespace TranscribeVideo.Core.DTOs.Summary
{
    public class CreateSummaryDto
    {
        public int TranscriptId { get; set; }

        public string SummaryText { get; set; } = string.Empty;

        public string? GeneratedBy { get; set; }
    }
}
