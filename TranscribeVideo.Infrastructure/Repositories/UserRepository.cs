using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (exist == null)
                throw new Exception("User not found.");

            return exist;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> AddAsync(Users entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Users> Update(Users entity, int id)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (exist == null)
                throw new Exception("User not found.");

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return exist;
        }

        public async Task<Users> Delete(int id)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (exist == null)
                throw new Exception("User not found.");

            _context.Users.Remove(exist);
            await _context.SaveChangesAsync();
            return exist;
        }


        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Users?> GetUserWithRoleAsync(int userId)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Users?> LoginUserAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.IsActive);
        }

        public async Task<IEnumerable<Users>> GetAllUsersWithRolesAsync()
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .ToListAsync();
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email);
        }
    }
}
