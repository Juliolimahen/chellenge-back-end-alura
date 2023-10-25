using FinancialControl.Core.Shared.Dtos.Summary;
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
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSummary([FromRoute] int year, [FromRoute] int month)
    {
        return Ok(await _summaryService.GetSummary(year, month));
    }
}
