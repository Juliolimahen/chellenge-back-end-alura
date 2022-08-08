using AutoMapper;
using FinancialControl.Context;
using FinancialControl.Dtos;
using FinancialControl.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialControl.Repositories.Interface
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public RevenueRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Revenue>> GetAll()
        {
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
}
