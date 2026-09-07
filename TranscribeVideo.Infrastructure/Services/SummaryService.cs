using TranscribeVideo.Core.DTOs.Summary;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;
using TranscribeVideo.Core.Mappers;

namespace TranscribeVideo.Infrastructure
{
    public class SummaryService : ISummaryService
    {
        private readonly ISummaryRepository _summaryRepository;

        public SummaryService(ISummaryRepository summaryRepository)
        {
            _summaryRepository = summaryRepository;
        }

        public async Task<SummaryResponseDto?> GetByTranscriptIdAsync(int transcriptId)
        {
            var summary = await _summaryRepository.GetByTranscriptIdAsync(transcriptId);

            return summary == null
                ? null
                : SummaryMapper.ToDto(summary);
        }
    }
}
