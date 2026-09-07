using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Infrastructure.Services
{
    public class FasterWhisperTranscriptionEngine : ITranscriptionEngine
    {
        private readonly string _pythonPath;
        private readonly string _scriptPath;
        private readonly string _modelSize;

        public FasterWhisperTranscriptionEngine(IConfiguration configuration)
        {
            _pythonPath = configuration["Whisper:PythonPath"] ?? "python3";

            var configuredScriptPath =
                configuration["Whisper:ScriptPath"]
                ?? "Script/transcribe_whisper.py";

            _scriptPath = Path.IsPathRooted(configuredScriptPath)
                ? configuredScriptPath
                : Path.Combine(AppContext.BaseDirectory, configuredScriptPath);

            _modelSize = configuration["Whisper:ModelSize"] ?? "base";

            if (!File.Exists(_scriptPath))
            {
                throw new FileNotFoundException(
                    $"Whisper script not found at '{_scriptPath}'. " +
                    "Set 'Whisper:ScriptPath' in appsettings.json to an absolute path, " +
                    "or make sure the script is copied to the output/publish directory " +
                    "(add it to the .csproj with CopyToOutputDirectory).",
                    _scriptPath);
            }
        }

        public async Task<TranscriptionResult> TranscribeAsync(
            string audioFilePath,
            string? language,
            IProgress<int>? progress = null)
        {
            var stopwatch = Stopwatch.StartNew();

            var languageCode =
                LanguageCodeMapper.ToIsoCode(language) ?? "auto";

            var psi = new ProcessStartInfo
            {
                FileName = _pythonPath,
                ArgumentList =
                {
                    _scriptPath,
                    audioFilePath,
                    languageCode,
                    _modelSize
                },
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi)
                ?? throw new InvalidOperationException(
                    "Failed to start whisper transcription process.");

            // Python will send progress through STDERR.
            // Final JSON result will be sent through STDOUT.
            var stdoutTask = process.StandardOutput.ReadToEndAsync();

            var stderrTask = ReadErrorOutputAsync(
                process,
                progress);

            await process.WaitForExitAsync();

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            stopwatch.Stop();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"Whisper transcription failed: {stderr}");
            }

            // Make sure the frontend receives 100% when transcription finishes.
            progress?.Report(100);

            using var doc = JsonDocument.Parse(stdout);

            var root = doc.RootElement;

            return new TranscriptionResult
            {
                Text = root.TryGetProperty("text", out var t)
                    ? t.GetString() ?? string.Empty
                    : string.Empty,

                Confidence =
                    root.TryGetProperty("confidence", out var c) &&
                    c.ValueKind != JsonValueKind.Null
                        ? c.GetDecimal()
                        : null,

                DetectedLanguage =
                    root.TryGetProperty("language", out var l)
                        ? l.GetString()
                        : null,

                ProcessingTimeInSeconds =
                    (int)stopwatch.Elapsed.TotalSeconds
            };
        }

        private static async Task<string> ReadErrorOutputAsync(
            Process process,
            IProgress<int>? progress)
        {
            var errorOutput = new System.Text.StringBuilder();

            while (!process.StandardError.EndOfStream)
            {
                var line = await process.StandardError.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                errorOutput.AppendLine(line);

                // Expected format:
                // PROGRESS:10
                // PROGRESS:25
                // PROGRESS:50
                // PROGRESS:100

                var match = Regex.Match(
                    line,
                    @"PROGRESS:(\d+)");

                if (match.Success &&
                    int.TryParse(match.Groups[1].Value, out var percentage))
                {
                    percentage = Math.Clamp(percentage, 0, 100);

                    progress?.Report(percentage);
                }
            }

            return errorOutput.ToString();
        }
    }
}