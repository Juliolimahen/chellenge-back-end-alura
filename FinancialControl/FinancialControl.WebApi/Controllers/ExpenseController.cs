﻿using FinancialControl.Core.Shared.Dtos;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [HttpGet("{id:int}", Name = "GetExpense")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [HttpGet("{year}/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
