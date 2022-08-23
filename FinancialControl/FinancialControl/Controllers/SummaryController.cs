using FinancialControl.Dtos;
using FinancialControl.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<ResponseDto<SummaryDto>>> GetResumoasync([FromRoute] int year, [FromRoute] int month)
        {
            return Ok(await _summaryService.GetSummary(year, month));
        }
    }
}
