using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.User;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto?> GetByIdAsync(int id);

        Task<UserResponseDto> CreateAsync(CreateUserDto dto);

        Task<UserResponseDto> UpdateAsync(UpdateUserDto dto, int id);

        Task<bool> DeleteAsync(int id);

    }
}
