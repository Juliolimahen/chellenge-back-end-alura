using FinancialControl.Dtos;
using FinancialControl.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Controllers
{
    public class SummaryController : Controller
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
