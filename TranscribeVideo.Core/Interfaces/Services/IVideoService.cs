using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Video;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IVideoService
    {
        Task<VideoResponseDto> UploadAsync(int userId, UploadVideoDto dto);
    }
}
