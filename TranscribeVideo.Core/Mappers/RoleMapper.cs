using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.Role;
using TranscribeVideo.Core.DTOs.User;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Mappers
{
    public static class RoleMapper
    {
        public static Roles ToEntity(CreateRoleDto dto)
        {
            return new Roles
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }

        public static RoleResponseDto ToDto(Roles role)
        {
            return new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
