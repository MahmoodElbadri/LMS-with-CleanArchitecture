using LMS.Core.DTOs.RequestDTOs;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(int id)
    {
        try
        {
            var book = await _bookSer.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookAddRequest bookAddRequest)
    {
        if (bookAddRequest == null)
        {
            return BadRequest("Book Data are invalid");
        }
        try
        {
            var bookResponse = await _bookSer.UpdateBookAsync(id, bookAddRequest);
            if (bookResponse == null) return NotFound();
            return Ok(bookResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error {ex.Message}");
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookAddRequest bookAddRequest)
    {
        if (bookAddRequest == null)
        {
            return BadRequest("Book data is invalid");
        }
        try
        {
            var book = await _bookSer.AddBookAsync(bookAddRequest);
            return CreatedAtAction(nameof(GetByID), new { id = book.ID }, book);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var book = await _bookSer.DeleteBookAsync(id);
            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
