using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Summary;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface ISummaryService
    {
        Task<SummaryResponseDto?> GetByTranscriptIdAsync(int transcriptId);
    }
}
