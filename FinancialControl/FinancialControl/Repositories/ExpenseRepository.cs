using AutoMapper;
using FinancialControl.Context;
using FinancialControl.Dtos;
using FinancialControl.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialControl.Repositories.Interface;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public ExpenseRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        return await _context.Expenses.ToListAsync();
    }

    public async Task<Expense> GetById(int id)
    {
        return await _context.Expenses.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Expense> Create(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> Update(Expense expense)
    {
        _context.Entry(expense).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> Delete(int id)
    {
        var expense = await GetById(id);
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> FirstOrDefaultAsync(Expression<Func<Expense, bool>> predicate)
    {
        return await _context.Set<Expense>().FirstOrDefaultAsync(predicate);
    }
}
