using FinancialControl.Core.Models;
using System.Linq.Expressions;

namespace FinancialControl.Data.Repositories.Interface
{
    public interface IRevenueRepository
    {
        Task<IEnumerable<Revenue>> GetAll(Expression<Func<Revenue, bool>>? predicate = null);
        Task<Revenue> GetById(int id);
        Task<Revenue> Create(Revenue revenue);
        Task<Revenue> Update(Revenue revenue);
        Task<Revenue> Delete(int id);
        Task<Revenue> FirstOrDefaultAsync(Expression<Func<Revenue, bool>> predicate);
    }
}
