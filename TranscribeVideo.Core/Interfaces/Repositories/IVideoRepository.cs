using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface IVideoRepository
    {
        Task<Videos> AddAsync(Videos entity);
    }
}
