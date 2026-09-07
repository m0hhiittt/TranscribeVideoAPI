using TranscribeVideo.Core.DTOs.Role;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;
using TranscribeVideo.Core.Mappers;

namespace TranscribeVideo.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(RoleMapper.ToDto);
        }

        public async Task<RoleResponseDto?> GetByIdAsync(int id)
        {
            var role = (await _roleRepository.GetAllAsync())
                        .FirstOrDefault(x => x.Id == id);

            return role == null ? null : RoleMapper.ToDto(role);
        }

        public async Task<RoleResponseDto> CreateAsync(CreateRoleDto dto)
        {
            var role = RoleMapper.ToEntity(dto);

            await _roleRepository.AddAsync(role);
            return RoleMapper.ToDto(role);
        }

        public async Task<RoleResponseDto> UpdateAsync(UpdateRoleDto dto, int id)
        {
            var role = (await _roleRepository.GetAllAsync())
                        .FirstOrDefault(x => x.Id == id);

            if (role == null)
                throw new Exception("Role not found.");

            role.Name = dto.Name;
            role.Description = dto.Description;

            await _roleRepository.Update(role, id);

            return RoleMapper.ToDto(role);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = (await _roleRepository.GetAllAsync())
                        .FirstOrDefault(x => x.Id == id);

            if (role == null)
                return false;

            await _roleRepository.Delete(id);
            return true;
        }
    }
}
