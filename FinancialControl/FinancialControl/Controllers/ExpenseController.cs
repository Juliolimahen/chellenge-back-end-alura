using FinancialControl.Dtos;
using FinancialControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

                //404 not found
                return NotFound("Expenses not found");

            //200OK
            return Ok(expensesDto);
        }

        // passando parametro e definindo nome
        [HttpGet("{id:int}", Name = "GetDespesa")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetById(int id)
        {
            var expenseDto = await _expenseService.GetExpenseById(id);
            if (expenseDto is null)

                //404 not found
                return NotFound("Expense not found");

            //200OK
            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateExpenseDto expenseDto)
        {
            if (expenseDto is null) { return BadRequest("Invalid Data"); }

            var expense = await _expenseService.CreateExpense(expenseDto);

            if (!expense.Success) { return BadRequest(expense); }

            //201 created
            //return new CreatedAtRouteResult("GetExpense", new { id = expense.Id },
            return Ok(expenseDto);
        }

        // tipo parâmetro
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ExpenseDto expenseDto)
        {
            if (id != expenseDto.Id) { return BadRequest(); }

            if (expenseDto is null) { return BadRequest(); }

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
            return expenses.Any() ? (ActionResult<IEnumerable<ExpenseDto>>)Ok(expenses) : (ActionResult<IEnumerable<ExpenseDto>>)NotFound("No expenses found on this date");
        }
    }
}
