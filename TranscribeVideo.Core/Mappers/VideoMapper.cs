using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Video;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Mappers
{
    public static class VideoMapper
    {
        public static VideoResponseDto ToDto(Videos video)
        {
            return new VideoResponseDto
            {
                Id = video.Id,
                OriginalFileName = video.OriginalFileName,
                FileSize = video.FileSize,
                DurationInSeconds = video.DurationInSeconds,
                Language = video.Language,
                Status = video.Status,
                UploadedAt = video.UploadedAt
            };
        }
    }
}
