using FinancialControl.Dtos;

namespace FinancialControl.Services;

public interface IExpenseService
{
    Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenses(string? description);
    Task<ResponseDto<IEnumerable<ExpenseDto>>> GetExpenseByDate(string year, string month);
    Task<ExpenseDto> GetExpenseById(int id);
    Task<ResponseDto<ExpenseDto>> CreateExpense(CreateExpenseDto expenseDto);
    Task<ResponseDto<ExpenseDto>> UpdateExpense(ExpenseDto expenseDto);
    Task DeleteExpense(int id);
}
