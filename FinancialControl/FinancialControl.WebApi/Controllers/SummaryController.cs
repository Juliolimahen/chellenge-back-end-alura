using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SummaryController : ControllerBase
{
    private readonly ISummaryService _summaryService;

    public SummaryController(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetResumoasync([FromRoute] int year, [FromRoute] int month)
    {
        return Ok(await _summaryService.GetSummary(year, month));
    }
}
