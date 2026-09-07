using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class TranscriptRepository : ITranscriptRepository
    {
        private readonly ApplicationDbContext _context;

        public TranscriptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transcript> AddAsync(Transcript entity)
        {
            await _context.Transcripts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Transcript?> GetByVideoIdAsync(int videoId)
        {
            return await _context.Transcripts
                .Include(x => x.Summaries)
                .FirstOrDefaultAsync(x => x.VideoId == videoId);
        }
    }
}
