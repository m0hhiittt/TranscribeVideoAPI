using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshTokens?> GetByIdAsync(int id)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);
            if (refreshToken == null)
                throw new Exception("Refresh token not found.");

            return refreshToken;
        }

        public async Task<IEnumerable<RefreshTokens>> GetAllAsync()
        {
            return await _context.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshTokens> AddAsync(RefreshTokens entity)
        {
            _context.RefreshTokens.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RefreshTokens> Update(RefreshTokens entity, int id)
        {
            var exist = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);
            if(exist == null)
                throw new Exception("Refresh token not found.");

            _context.RefreshTokens.Update(entity);
            await _context.SaveChangesAsync();
            return exist;
        }

        public async Task<RefreshTokens> Delete(int id)
        {
            var exist = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);
            if(exist == null)
                throw new Exception("Refresh token not found.");

            _context.RefreshTokens.Remove(exist);
            await _context.SaveChangesAsync();
            return exist;
        }

        public async Task<RefreshTokens?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<IEnumerable<RefreshTokens>> GetUserTokensAsync(int userId)
        {
            return await _context.RefreshTokens
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
