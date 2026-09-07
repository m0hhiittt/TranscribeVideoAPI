using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("Summaries")]
    public class Summaries
    {
        public int Id { get; set; }

        public int TranscriptId { get; set; }

        public string SummaryText { get; set; } = string.Empty;

        public string? GeneratedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Transcript Transcript { get; set; } = null!;
    }
}
