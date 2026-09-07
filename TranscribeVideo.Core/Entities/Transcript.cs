using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("Transcripts")]
    public class Transcript
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public string TranscriptText { get; set; } = string.Empty;

        public decimal? ConfidenceScore { get; set; }

        public int? ProcessingTimeInSeconds { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Videos Video { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<Summaries> Summaries { get; set; } = new List<Summaries>();
    }
}
