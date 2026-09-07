using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TranscribeVideo.Core.DTOs.Transcript;
using TranscribeVideo.Core.DTOs.Video;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;
using TranscribeVideo.Core.Mappers;

namespace TranscribeVideo.Infrastructure.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ITranscriptRepository _transcriptRepository;
        private readonly ITranscriptionEngine _transcriptionEngine;
        private readonly string _storageRoot;
        private readonly ILogger<VideoService> _logger;


        public VideoService(
            IVideoRepository videoRepository,
            ITranscriptRepository transcriptRepository,
            ITranscriptionEngine transcriptionEngine,
            IConfiguration configuration,
            ILogger<VideoService> logger)
        {
            _videoRepository = videoRepository;
            _transcriptRepository = transcriptRepository;
            _transcriptionEngine = transcriptionEngine;
            _storageRoot = configuration["Storage:RootPath"]
                ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            _logger = logger;
        }

        public async Task<VideoResponseDto> UploadAsync(int userId, UploadVideoDto dto)
        {
            var file = dto.VideoFile;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var videosFolder = Path.Combine(_storageRoot, "videos");
            Directory.CreateDirectory(videosFolder);
            var videoFullPath = Path.Combine(videosFolder, fileName);

            await using (var stream = new FileStream(videoFullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var video = new Videos
            {
                UserId = userId,
                OriginalFileName = file.FileName,
                StoredFileName = fileName,
                FileExtension = Path.GetExtension(file.FileName),
                FileSize = file.Length,
                Language = dto.Language,
                StoragePath = $"uploads/videos/{fileName}",
                Status = "Processing",
                UploadedAt = DateTime.UtcNow
            };

            await _videoRepository.AddAsync(video);
            TranscriptResponseDto? transcriptDto = null;
            string? audioPath = null;

            try
            {
                var audioFolder = Path.Combine(_storageRoot, "audio");
                var altAudioFolder = Path.Combine(_storageRoot, "audo");

                if (!Directory.Exists(audioFolder) && Directory.Exists(altAudioFolder))
                {
                    audioFolder = altAudioFolder;
                }

                Directory.CreateDirectory(audioFolder);
                _logger.LogInformation("Using audio folder: {AudioFolder}", audioFolder);

                audioPath = await AudioExtractor.ExtractAudioAsync(videoFullPath, audioFolder);

                var result = await _transcriptionEngine.TranscribeAsync(audioPath, dto.Language);

                var transcript = new Transcript
                {
                    VideoId = video.Id,
                    TranscriptText = result.Text,
                    ConfidenceScore = result.Confidence,
                    ProcessingTimeInSeconds = result.ProcessingTimeInSeconds,
                    CreatedAt = DateTime.UtcNow
                };

                await _transcriptRepository.AddAsync(transcript);
                transcriptDto = TranscriptMapper.ToDto(transcript);

                video.Status = "Completed";
                video.UpdatedAt = DateTime.UtcNow;
            }
            catch (Exception)
            {
                video.Status = "Failed";
                video.UpdatedAt = DateTime.UtcNow;
                _logger.LogError("Transcription failed for video {VideoId}", video.Id);

            }
            finally
            {
                if (videoFullPath != null && File.Exists(videoFullPath))
                {
                    File.Delete(videoFullPath);
                }
            }

            return new VideoResponseDto
            {
                Id = video.Id,
                OriginalFileName = video.OriginalFileName,
                FileSize = video.FileSize,
                DurationInSeconds = video.DurationInSeconds,
                Language = video.Language,
                Status = video.Status,
                UploadedAt = video.UploadedAt,
                Video = VideoMapper.ToDto(video),
                Transcript = transcriptDto
            };
        }
    }
}