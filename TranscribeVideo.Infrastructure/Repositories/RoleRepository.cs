using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Data;
using TranscribeVideo.Core.Entities;
using TranscribeVideo.Core.Interfaces.Repository;
namespace TranscribeVideo.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Roles?> GetByIdAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if(role == null) 
                throw new Exception("Role not found.");
            return role;
        }

        public async Task<IEnumerable<Roles>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Roles> AddAsync(Roles entity)
        {            
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Roles> Update(Roles entity, int id)
        {
            var exist = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (exist == null)
                throw new Exception("Role not found.");
            _context.Roles.Update(exist);
            await _context.SaveChangesAsync();
            return exist;
        }

        public async Task<Roles> Delete(int id)
        {
            var exist = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (exist == null)
                throw new Exception("Role not found.");
            _context.Roles.Remove(exist);
            await _context.SaveChangesAsync();
            return exist;
        }
    }
}
