using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Expense;

namespace FinancialControl.Manager.Services.Interface;

public interface IExpenseService
{
    Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpensesAsync(string? description);
    Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenseByDateAsync(string year, string month);
    Task<ExpenseDto> GetExpenseByIdAsync(int id);
    Task<ResponseDto<ExpenseDto>> CreateExpenseAsync(CreateExpenseDto expenseDto);
    Task<ResponseDto<ExpenseDto>> UpdateExpenseAsync(ExpenseDto expenseDto);
    Task DeleteExpenseAsync(int id);
}
