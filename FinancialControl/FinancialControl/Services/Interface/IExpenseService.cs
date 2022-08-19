using FinancialControl.Dtos;

namespace FinancialControl.Services;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDto>> GetExpenses(string? description);
    Task<ExpenseDto> GetExpenseById(int id);
    Task<ResponseDto<ExpenseDto>> CreateExpense(CreateExpenseDto expenseDto);
    Task<ResponseDto<ExpenseDto>> UpdateExpense(ExpenseDto expenseDto);
    Task DeleteExpense(int id);
}
