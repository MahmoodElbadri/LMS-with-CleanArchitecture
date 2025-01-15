using LMS.Core.Interfaces.Repositories;
using LMS.Core.Models;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories;

public class UsersRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<UsersRepository> logger;

    public UsersRepository(ApplicationDbContext db, ILogger<UsersRepository> logger)
    {
        this._db = db;
        this.logger = logger;
    }
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while adding a user");
            throw;
        }
    }

    public Task DeleteUserAsync(int id)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(tmp => tmp.ID == id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }
            else
            {
                _db.Users.Remove(user);
                return _db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting a user");
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await _db.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting all users");
            throw;
        }
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            return await _db.Users.FirstAsync(tmp => tmp.ID == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting a user by ID");
            throw;
        }
    }

    public async Task UpdateUserAsync(int id, User user)
    {
        try
        {
            var existUser = await GetUserByIdAsync(id);
            if (existUser == null)
            {
                throw new Exception($"User with ID {id} not found");
            }
            else
            {
                existUser.FirstName = user.FirstName;
                existUser.LastName = user.LastName;
                await _db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating a user");
            throw;
        }
    }
}
