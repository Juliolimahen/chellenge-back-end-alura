using FinancialControl.Core.Models;
using FinancialControl.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialControl.Data.Repositories.Interface;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<TEntity> GetByIdAsync(int id);

    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(int id);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}