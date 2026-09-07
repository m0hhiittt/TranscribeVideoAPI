using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.ProcessJob;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IProcessingJobService
    {
        Task<IEnumerable<ProcessingJobResponseDto>> GetQueuedJobsAsync();

        Task<ProcessingJobResponseDto?> GetByVideoIdAsync(int videoId);
    }
}
