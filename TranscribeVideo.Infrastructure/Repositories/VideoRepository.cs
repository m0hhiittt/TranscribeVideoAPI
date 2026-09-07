using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Videos> AddAsync(Videos entity)
        {
            await _context.Videos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
