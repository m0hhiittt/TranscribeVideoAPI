using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface ISummaryRepository
    {
       Task<Summaries?> GetByTranscriptIdAsync(int transcriptId);
    }
}
