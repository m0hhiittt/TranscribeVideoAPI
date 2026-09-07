using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Transcript;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface ITranscriptService
    {
        Task<TranscriptResponseDto?> GetByVideoIdAsync(int videoId);

        Task<TranscriptResponseDto> CreateAsync(CreateTranscriptDto dto);
    }
}
