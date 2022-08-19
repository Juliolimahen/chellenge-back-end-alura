using FinancialControl.Dtos;
using FinancialControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevenueDto>>> GetAll([FromQuery] string? description)
        {
            var revenuesDto = await _revenueService.GetRevenues(description);
            if (revenuesDto is null)

                //404 not found
                return NotFound("Revenues not found");

            //200OK
            return Ok(revenuesDto);
        }

        // passando parametro e definindo nome
        [HttpGet("{id:int}", Name = "GetReceita")]
        public async Task<ActionResult<IEnumerable<RevenueDto>>> GetById(int id)
        {
            var revenueDto = await _revenueService.GetRevenueById(id);
            if (revenueDto is null)

                //404 not found
                return NotFound("Revenue not found");

            //200OK
            return Ok(revenueDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRevenueDto revenueDto)
        {
            if (revenueDto is null) { return BadRequest("Invalid Data"); }

            var revenue = await _revenueService.CreateRevenue(revenueDto);

            if (!revenue.Success) { return BadRequest(revenue); }

            //201 created
            //return new CreatedAtRouteResult("GetRevenue", new { id = revenue.Id },
            return Ok(revenueDto);
        }

        // tipo parâmetro
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] RevenueDto revenueDto)
        {
            if (id != revenueDto.Id) { return BadRequest(); }

            if (revenueDto is null) { return BadRequest(); }

            var revenue = await _revenueService.UpdateRevenue(revenueDto);

            if (!revenue.Success) { return BadRequest(revenue); }

            return Ok(revenueDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var revenueDto = await _revenueService.GetRevenueById(id);
            if (revenueDto is null)
            {
                return NotFound("Revenue not found");
            }

            await _revenueService.DeleteRevenue(id);
            return Ok(revenueDto);
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<IEnumerable<RevenueDto>>> GetAllExpenseByDate([FromRoute] string year, [FromRoute] string month)
        {
            var revenues = await _revenueService.GetRevenueByDate(year, month);
            return revenues.Data == null ? (ActionResult<IEnumerable<RevenueDto>>)Ok(revenues) : (ActionResult<IEnumerable<RevenueDto>>)NotFound("No revenues found on this date");
        }
    }
}
