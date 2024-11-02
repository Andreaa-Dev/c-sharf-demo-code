using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.Utils;
using static user.src.DTO.UserDTO;

namespace user.src.Services.user
{
    public interface IUserService
    {
        // only need this
        Task<UserReadDto> CreateOneAsync(UserCreateDto createDto);
        Task<bool> EmailExistsAsync(string email);
        Task<UserReadDto> CreateAdminAsync(UserCreateDto createDto);
        Task<string> SignInAsync(UserSignInDto userSignInDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<IEnumerable<UserReadDto>> GetAllAsync(PaginationOptions getAllOptions);
        Task<UserReadDto> GetByIdAsync(Guid id);
        Task<UserReadDto> UpdateOneAsync(Guid id, UserUpdateDto updateDto);
        Task<bool> UpdateAdminAsync(Guid id);
        Task<UserReadDto> FindByEmailAsync(string email);
    }
}