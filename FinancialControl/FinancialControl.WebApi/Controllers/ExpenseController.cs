using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<ExpenseDto>>> GetAll([FromQuery] string? description)
    {
        var expenses = await _expenseService.GetExpensesAsync(description);
        if (expenses is null || !expenses.Data.Any())
            return NotFound(expenses == null ? "Expenses not found for this Id." : "Expenses not found for this description.");

        return Ok(expenses);
    }

    [HttpGet("{id:int}", Name = "GetDespesa")]
    public async Task<IActionResult> GetById(int id)
    {
        var expenseDto = await _expenseService.GetExpenseByIdAsync(id);
        if (expenseDto is null)
            return NotFound("Expense not found for this Id.");

        return Ok(expenseDto);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto<ExpenseDto>>> Create([FromBody] CreateExpenseDto expenseDto)
    {
        if (expenseDto is null)
            return BadRequest("Invalid revenue data. Please provide valid data.");

        var expense = await _expenseService.CreateExpenseAsync(expenseDto);

        if (!expense.Success)
            return BadRequest(expense);

        return CreatedAtAction(nameof(GetById), new { id = expenseDto.Id }, expenseDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExpenseDto expenseDto)
    {
        if (expenseDto == null || id != expenseDto.Id)
            return BadRequest(expenseDto == null ?
                "Invalid expense data. Please provide valid data."
                : "The provided ID does not match the expense ID.");

        var updateResult = await _expenseService.UpdateExpenseAsync(expenseDto);

        return updateResult.Success
            ? Ok(expenseDto)
            : BadRequest(updateResult);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var expenseDto = await _expenseService.GetExpenseByIdAsync(id);
        if (expenseDto is null)
            return NotFound($"Expense not found for this Id.");

        await _expenseService.DeleteExpenseAsync(id);
        return Ok(expenseDto);
    }

    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetAllExpenseByDate([FromRoute] string year, [FromRoute] string month)
    {
        var expenses = await _expenseService.GetExpenseByDateAsync(year, month);
        if (expenses is null)
            return NotFound("Expense not found for this date.");

        return Ok(expenses);
    }
}
