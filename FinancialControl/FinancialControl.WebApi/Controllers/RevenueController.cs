using FinancialControl.Core.Shared.Dtos.Revenue;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? description)
    {
        var revenuesDto = await _revenueService.GetRevenuesAsync(description);
        if (revenuesDto is null)
            return NotFound("Revenues not found for this description");

        return Ok(revenuesDto);
    }

    [HttpGet("{id:int}", Name = "GetReceita")]
    public async Task<IActionResult> GetById(int id)
    {
        var revenueDto = await _revenueService.GetRevenueByIdAsync(id);
        if (revenueDto is null)
            return NotFound("Revenue not found for this Id");

        return Ok(revenueDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRevenueDto revenueDto)
    {
        if (revenueDto is null)
            return BadRequest("Invalid revenue data. Please provide valid data.");

        var revenue = await _revenueService.CreateRevenueAsync(revenueDto);

        if (!revenue.Success)
            return BadRequest(revenue);

        return CreatedAtAction(nameof(GetById), new { id = revenueDto.Id }, revenueDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RevenueDto revenueDto)
    {
        if (revenueDto == null || id != revenueDto.Id)
            return BadRequest(revenueDto == null ?
                "Invalid revenue data. Please provide valid data."
                : "The provided ID does not match the revenue ID.");

        var updateResult = await _revenueService.UpdateRevenueAsync(revenueDto);

        return updateResult.Success
            ? Ok(revenueDto)
            : BadRequest(updateResult);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var revenueDto = await _revenueService.GetRevenueByIdAsync(id);
        if (revenueDto is null)
        {
            return NotFound("Revenue not found");
        }

        await _revenueService.DeleteRevenueAsync(id);
        return Ok(revenueDto);
    }

    [HttpGet("{year}/{month}")]
    public async Task<ActionResult<IEnumerable<RevenueDto>>> GetAllExpenseByDate([FromRoute] string year, [FromRoute] string month)
    {
        var revenues = await _revenueService.GetRevenueByDateAsync(year, month);
        return Ok(revenues);
    }
}
