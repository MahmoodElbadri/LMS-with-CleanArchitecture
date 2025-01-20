using LMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ILogger<IBooksService> _logger;
    private readonly IBooksService _bookSer;

    public BooksController(ILogger<IBooksService> logger, IBooksService bookSer)
    {
        this._logger = logger;
        this._bookSer = bookSer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("Get All Books");
            var books = await _bookSer.GetAllBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get All Books Error");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
