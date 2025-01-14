using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces.Services;

public interface IUsersService
{
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task<UserResponse> GetUserByIdAsync(int id);
    Task<UserResponse> AddUserAsync(UserAddRequest userDto);
    Task UpdateUserAsync(int id, UserAddRequest userDto);
    Task DeleteUserAsync(int id);
}
