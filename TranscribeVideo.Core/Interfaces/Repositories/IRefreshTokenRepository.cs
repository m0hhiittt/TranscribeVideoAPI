using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokens?> GetByIdAsync(int id);

        Task<IEnumerable<RefreshTokens>> GetAllAsync();

        Task<RefreshTokens>  AddAsync(RefreshTokens entity);

        Task<RefreshTokens> Update(RefreshTokens entity, int id);

        Task<RefreshTokens> Delete(int id);

        Task<RefreshTokens?> GetByTokenAsync(string token);

        Task<IEnumerable<RefreshTokens>> GetUserTokensAsync(int userId);
    }
}
