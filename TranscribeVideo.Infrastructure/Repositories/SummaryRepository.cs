using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly ApplicationDbContext _context;

        public SummaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Summaries?> GetByTranscriptIdAsync(int transcriptId)
        {
            return await _context.Summaries
                .FirstOrDefaultAsync(x => x.TranscriptId == transcriptId);
        }
    }
}
