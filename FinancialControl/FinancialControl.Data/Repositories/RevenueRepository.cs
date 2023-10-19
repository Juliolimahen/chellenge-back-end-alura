using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Data.Context;
using FinancialControl.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialControl.Data.Repositories;

public class RevenueRepository : IRevenueRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public RevenueRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Revenue>> GetAll(Expression<Func<Revenue, bool>>? predicate = null)
    {
        if (predicate is not null)
            return await _context.Set<Revenue>().Where(predicate).ToListAsync();

        return await _context.Revenues.ToListAsync();
    }

    public async Task<Revenue> GetById(int id)
    {
        return await _context.Revenues.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Revenue> Create(Revenue revenue)
    {
        _context.Revenues.Add(revenue);
        await _context.SaveChangesAsync();
        return revenue;
    }

    public async Task<Revenue> Update(Revenue revenue)
    {
        _context.Entry(revenue).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return revenue;
    }

    public async Task<Revenue> Delete(int id)
    {
        var revenue = await GetById(id);
        _context.Revenues.Remove(revenue);
        await _context.SaveChangesAsync();
        return revenue;
    }

    public async Task<Revenue> FirstOrDefaultAsync(Expression<Func<Revenue, bool>> predicate)
    {
        return await _context.Set<Revenue>().FirstOrDefaultAsync(predicate);
    }
}
