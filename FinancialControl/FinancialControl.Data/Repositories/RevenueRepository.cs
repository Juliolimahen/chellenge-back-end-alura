using FinancialControl.Core.Models;
using FinancialControl.Data.Context;
using FinancialControl.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Data.Repositories;

public class RevenueRepository : BaseRepository<Revenue>, IRevenueRepository
{
    public RevenueRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
