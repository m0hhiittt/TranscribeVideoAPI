using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("ProcessingJobs")]
    public class ProcessingJobs
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public string Status { get; set; } = "Queued";

        public int RetryCount { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Videos Video { get; set; } = null!;
    }
}
