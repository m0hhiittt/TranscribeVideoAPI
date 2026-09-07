using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Role;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponseDto>> GetAllAsync();

        Task<RoleResponseDto?> GetByIdAsync(int id);

        Task<RoleResponseDto> CreateAsync(CreateRoleDto dto);

        Task<RoleResponseDto> UpdateAsync(UpdateRoleDto dto, int id);

        Task<bool> DeleteAsync(int id);
    }
}
