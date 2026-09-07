using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TranscribeVideo.Infrastructure.Services
{
    public static class AudioExtractor
    {
        public static async Task<string> ExtractAudioAsync(string videoPath, string outputDir)
        {
            Directory.CreateDirectory(outputDir);

            var audioPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(videoPath)}.wav");

            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",    
                ArgumentList = { "-i", videoPath, "-ar", "16000", "-ac", "1", "-vn", "-y", audioPath },
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi)
                ?? throw new InvalidOperationException("Failed to start ffmpeg process.");

            var stderrTask = process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();
            var stderr = await stderrTask;

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"ffmpeg audio extraction failed: {stderr}");
            }

            return audioPath;
        }
    }
}
