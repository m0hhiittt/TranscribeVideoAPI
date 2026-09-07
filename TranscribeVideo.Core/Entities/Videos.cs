using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TranscribeVideo.Core.Entities
{
    [Table("Videos")]
    public class Videos
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string? FileExtension { get; set; }

        public long FileSize { get; set; }

        public int? DurationInSeconds { get; set; }

        public string? Language { get; set; }

        public string StoragePath { get; set; } = string.Empty;

        public string? ThumbnailPath { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Users User { get; set; } = null!;

        public ICollection<ProcessingJobs> ProcessingJobs { get; set; } = new List<ProcessingJobs>();

        public ICollection<Transcript> Transcripts { get; set; } = new List<Transcript>();
    }
}
