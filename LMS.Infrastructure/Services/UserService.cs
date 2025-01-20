using AutoMapper;
using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using LMS.Core.Interfaces.Repositories;
using LMS.Core.Interfaces.Services;
using LMS.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services;

public class UserService : IUsersService
{
    private readonly ILogger<UserService> _logger; // Fixed: Changed to UserService
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public UserService(ILogger<UserService> logger, IMapper mapper, IUserRepository userRepo) // Fixed: Changed to UserService
    {
        this._logger = logger;
        this._mapper = mapper;
        this._userRepo = userRepo;
    }

    public async Task<UserResponse> AddUserAsync(UserAddRequest userDto)
    {
        if (userDto == null) throw new ArgumentNullException(nameof(userDto), "User cannot be null");
        try
        {
            var user = _mapper.Map<User>(userDto);
            var userReponse = await _userRepo.AddUserAsync(user);
            var userToReturn = _mapper.Map<UserResponse>(userReponse);
            return userToReturn;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a user");
            throw;
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        if (id <= 0) throw new ArgumentException($"Argument should be greater than {id}");
        try
        {
            await _userRepo.DeleteUserAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting user with id {id}");
            throw;
        }
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepo.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all users");
            throw;
        }
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Argument should be greater than {id}");
        }
        try
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting user with id {id}");
            throw;
        }
    }

    public async Task UpdateUserAsync(int id, UserAddRequest userDto)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"{nameof(userDto)} should be greater than {id}");
        }
        if (userDto == null) throw new ArgumentNullException(nameof(userDto), "User cannot be null");
        try
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepo.UpdateUserAsync(id, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating user with id {id}");
            throw;
        }
    }
}