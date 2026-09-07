using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<Users?> GetByIdAsync(int id);

        Task<IEnumerable<Users>> GetAllAsync();

        Task<Users> AddAsync(Users entity);

        Task<Users> Update(Users entity, int id);

        Task<Users> Delete(int id);


        Task<Users?> GetByEmailAsync(string email);

        Task<Users?> GetUserWithRoleAsync(int userId);

        Task<Users?> LoginUserAsync(string email);

        Task<IEnumerable<Users>> GetAllUsersWithRolesAsync();

        Task<bool> EmailExistsAsync(string email);
    }
}
