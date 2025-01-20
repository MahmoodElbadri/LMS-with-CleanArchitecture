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
}
