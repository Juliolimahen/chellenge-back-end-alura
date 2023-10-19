using FinancialControl.Core.Models;
using FinancialControl.Data.Context;
using FinancialControl.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialControl.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        return entity;
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => 
        await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = _dbSet.AsQueryable();
        if (predicate != null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = _dbSet.AsQueryable();
        if (predicate != null)
            query = query.Where(predicate);

        return await query.CountAsync();
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _dbSet.Remove(entity);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id) => 
        await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
