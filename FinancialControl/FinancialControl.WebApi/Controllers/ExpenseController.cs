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

    /// <summary>
    /// End point responsavel por buscar todas as despesas cadastradas, sendo possivel buscar por uma descrição especifica.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromQuery] string? description)
    {
        var response = await _expenseService.GetExpensesAsync(description);

        if (response.Success && response.Data.Any())
        {
            return Ok(response);
        }

        return NotFound(new ResponseDto<IEnumerable<ExpenseDto>>
        {
            Success = false,
            Erros = new List<string> { "No expenses found for this description." }
        });
    }

    /// <summary>
    /// End point responsavel por buscar uma despesa por Id. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetExpense")]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _expenseService.GetExpenseByIdAsync(id);

        if (response.Data != null)
        {
            return Ok(response);
        }

        return NotFound(new ResponseDto<ExpenseDto>
        {
            Success = false,
            Erros = new List<string> { "Expense not found for this Id." }
        });
    }

    /// <summary>
    /// End point responsavel por casdatrar um nova despesa. 
    /// </summary>
    /// <param name="expenseDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateExpenseDto expenseDto)
    {
        if (expenseDto == null)
        {
            return BadRequest(new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string> { "Invalid expense data. Please provide valid data." }
            });
        }

        var response = await _expenseService.CreateExpenseAsync(expenseDto);

        if (response.Success)
        {
            return CreatedAtAction("GetById", new { id = response.Data.Id }, response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// End point responsavel por editar uma despesa. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expenseDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] ExpenseDto expenseDto)
    {
        if (expenseDto == null || id != expenseDto.Id)
        {
            return BadRequest(new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string> { "Invalid expense data. Please provide valid data." }
            });
        }

        var response = await _expenseService.UpdateExpenseAsync(expenseDto);

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// End point responsavel por deletar uma despesa.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _expenseService.GetExpenseByIdAsync(id);

        if (response.Data != null)
        {
            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }

        return NotFound(new ResponseDto<ExpenseDto>
        {
            Success = false,
            Erros = new List<string> { "Expense not found." }
        });
    }

    /// <summary>
    /// End point responsavel por buscar as despesas por mês e ano.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    [HttpGet("{year}/{month}")]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto<ExpenseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllExpenseByDate([FromRoute] string year, [FromRoute] string month)
    {
        var response = await _expenseService.GetExpenseByDateAsync(year, month);

        if (response.Success && response.Data.Any())
        {
            return Ok(response);
        }

        return NotFound(new ResponseDto<IEnumerable<ExpenseDto>>
        {
            Success = false,
            Erros = new List<string> { "No expenses found for this date." }
        });
    }
}
