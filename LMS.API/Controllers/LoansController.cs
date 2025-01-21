using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly ILogger<LoansController> _logger;
    private readonly ILoansService _loansSer;

    public LoansController(ILogger<LoansController> logger, ILoansService loansSer)
    {
        this._logger = logger;
        this._loansSer = loansSer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        try
        {
            _logger.LogInformation("Get All Loans");
            var loans = await _loansSer.GetAllLoansAsync();
            return Ok(loans);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get All Loans Error");
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoanById([FromRoute] int id)
    {
        try
        {
            var loan = await _loansSer.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound($"Book with id: {id} not found");
            }
            return Ok(loan);
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
    public async Task<IActionResult> CreateLoan([FromBody] LoanAddRequest loanAddRequest)
    {
        try
        {
            var loan = await _loansSer.AddLoanAsync(loanAddRequest);
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.ID }, loan);
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
    public async Task<IActionResult> UpdateLoan([FromBody] LoanAddRequest loanAddRequest, [FromRoute] int id)
    {
        try
        {
            var loan = await _loansSer.UpdateLoanAsync(id, loanAddRequest);
            return Ok(loan);
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
    public async Task<IActionResult> DeleteLoan([FromRoute] int id)
    {
        try
        {
            await _loansSer.DeleteLoanAsync(id);
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
