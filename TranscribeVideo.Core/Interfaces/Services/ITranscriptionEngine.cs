using System.Threading.Tasks;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface ITranscriptionEngine
    {
        Task<TranscriptionResult> TranscribeAsync(
     string audioFilePath,
     string? language,
     IProgress<int>? progress = null);
    }

    public class TranscriptionResult
    {
        public string Text { get; set; } = string.Empty;
        public decimal? Confidence { get; set; }
        public int ProcessingTimeInSeconds { get; set; }
        public string? DetectedLanguage { get; set; }
    }
}
