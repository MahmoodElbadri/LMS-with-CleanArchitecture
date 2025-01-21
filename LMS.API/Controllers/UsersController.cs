using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _usersSer;

    public UsersController(ILogger<UsersController> logger, IUsersService usersSer)
    {
        this._logger = logger;
        this._usersSer = usersSer;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            _logger.LogInformation("Get All Users");
            var users = await _usersSer.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get All Users Error");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        try
        {
            var user = await _usersSer.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with id: {id} not found");
            }
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserAddRequest userAddRequest)
    {
        try
        {
            var user = await _usersSer.AddUserAsync(userAddRequest);
            return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserAddRequest userAddRequest)
    {
        try
        {
            await _usersSer.UpdateUserAsync(id, userAddRequest);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await _usersSer.DeleteUserAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
