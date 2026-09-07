using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Transcript;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Mappers
{
    public static class TranscriptMapper
    {
        public static Transcript ToEntity(CreateTranscriptDto dto)
        {
            return new Transcript
            {
                Id = dto.VideoId,
                VideoId = dto.VideoId,
                TranscriptText = dto.TranscriptText,
                ConfidenceScore = dto.ConfidenceScore,
                ProcessingTimeInSeconds = dto.ProcessingTimeInSeconds,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static TranscriptResponseDto ToDto(Transcript transcript)
        {
            return new TranscriptResponseDto
            {
                Id = transcript.Id,
                VideoId = transcript.VideoId,
                TranscriptText = transcript.TranscriptText,
                ConfidenceScore = transcript.ConfidenceScore,
                ProcessingTimeInSeconds = transcript.ProcessingTimeInSeconds,
                CreatedAt = transcript.CreatedAt
            };
        }
    }
}
