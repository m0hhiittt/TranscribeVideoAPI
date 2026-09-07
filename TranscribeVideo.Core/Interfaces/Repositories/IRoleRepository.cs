using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface IRoleRepository
    {
        Task<Roles?> GetByIdAsync(int id);

        Task<IEnumerable<Roles>> GetAllAsync();

        Task<Roles> AddAsync(Roles entity);

        Task<Roles> Update(Roles entity, int id);

        Task<Roles> Delete(int id);
    }
}
