using FinancialControl.Core.Models;
using System.Linq.Expressions;

namespace FinancialControl.Data.Repositories.Interface
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAll(Expression<Func<Expense, bool>>? predicate = null);
        Task<Expense> GetById(int id);
        Task<Expense> Create(Expense expense);
        Task<Expense> Update(Expense expense);
        Task<Expense> Delete(int id);
        Task<Expense> FirstOrDefaultAsync(Expression<Func<Expense, bool>> predicate);
    }
}
