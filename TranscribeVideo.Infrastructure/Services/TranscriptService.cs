using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Transcript;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;
using TranscribeVideo.Core.Mappers;

namespace TranscribeVideo.Infrastructure.Services
{
    public class TranscriptService : ITranscriptService
    {
        private readonly ITranscriptRepository _transcriptRepository;

        public TranscriptService(ITranscriptRepository transcriptRepository)
        {
            _transcriptRepository = transcriptRepository;
        }

        public async Task<TranscriptResponseDto> CreateAsync(CreateTranscriptDto dto)
        {
            var transcript = TranscriptMapper.ToEntity(dto);

            await _transcriptRepository.AddAsync(transcript);
            return TranscriptMapper.ToDto(transcript);
        }

        public async Task<TranscriptResponseDto?> GetByVideoIdAsync(int videoId)
        {
            var transcript = await _transcriptRepository.GetByVideoIdAsync(videoId);

            return transcript == null
                ? null
                : TranscriptMapper.ToDto(transcript);
        }
    }
}
