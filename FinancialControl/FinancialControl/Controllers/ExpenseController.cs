using FinancialControl.Core.Shared.Dtos;
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
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll([FromQuery] string? description)
    {
        var expensesDto = await _expenseService.GetExpenses(description);
        if (expensesDto is null)
            return NotFound("Expenses not found");

        return Ok(expensesDto);
    }

    [HttpGet("{id:int}", Name = "GetDespesa")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetById(int id)
    {
        var expenseDto = await _expenseService.GetExpenseById(id);
        if (expenseDto is null)

            return NotFound("Expense not found");

        return Ok(expenseDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateExpenseDto expenseDto)
    {
        if (expenseDto is null) { return BadRequest("Invalid Data"); }

        var expense = await _expenseService.CreateExpense(expenseDto);

        if (!expense.Success) { return BadRequest(expense); }

        return CreatedAtAction(nameof(GetById), new { id = expenseDto.Id }, expenseDto);
        //return Ok(expenseDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExpenseDto expenseDto)
    {
        if (id != expenseDto.Id || expenseDto is null) { return BadRequest(); }

        var expense = await _expenseService.UpdateExpense(expenseDto);

        if (!expense.Success) { return BadRequest(expense); }
        return Ok(expenseDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var expenseDto = await _expenseService.GetExpenseById(id);
        if (expenseDto is null)
        {
            return NotFound("Expense not found");
        }

        await _expenseService.DeleteExpense(id);
        return Ok(expenseDto);
    }

    [HttpGet("{year}/{month}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenseByDate([FromRoute] string year, [FromRoute] string month)
    {
        var expenses = await _expenseService.GetExpenseByDate(year, month);
        return Ok(expenses);
    }
}
