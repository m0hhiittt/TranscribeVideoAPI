using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface ITranscriptRepository
    {
        Task<Transcript> AddAsync(Transcript entity);

        Task<Transcript?> GetByVideoIdAsync(int videoId);
    }
}
