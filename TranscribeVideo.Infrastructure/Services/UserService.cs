using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.DTOs.User;
using TranscribeVideo.Core.Interfaces.Repository;
using TranscribeVideo.Core.Interfaces.Services;
using TranscribeVideo.Core.Mappers;

namespace TranscribeVideo.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllUsersWithRolesAsync();

            return users.Select(UserMapper.ToDto).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetUserWithRoleAsync(id);

            if (user == null)
                return null;

            return UserMapper.ToDto(user);
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
                throw new Exception("Email already exists.");

            var user = UserMapper.ToEntity(dto);

            await _userRepository.AddAsync(user);
            return UserMapper.ToDto(user);
        }

        public async Task<UserResponseDto> UpdateAsync(UpdateUserDto dto, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found.");

            UserMapper.UpdateEntity(user, dto);

            await _userRepository.Update(user, id);

            return UserMapper.ToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.Delete(id);
            return true;
        }

    }

}