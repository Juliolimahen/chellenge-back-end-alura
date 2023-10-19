using FinancialControl.Core.Models;
using FinancialControl.Data.Context;
using FinancialControl.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Data.Repositories;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
