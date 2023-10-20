using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Summary;
using FinancialControl.Data.Repositories.Interface;
using FinancialControl.Manager.Services.Interface;
using System.Linq.Expressions;

namespace FinancialControl.Manager.Services;

public class SummaryService : ISummaryService
{

    private readonly IRevenueRepository _revenueRepository;
    private readonly IExpenseRepository _expenseRepository;


    public SummaryService(IExpenseRepository expenseRepository, IRevenueRepository revenueRepository)
    {
        _expenseRepository = expenseRepository;
        _revenueRepository = revenueRepository;
    }

    public async Task<ResponseDto<SummaryDto>> GetSummary(int year, int month)
    {

        Expression<Func<Revenue, bool>> revenueExpression = x => x.Date.Year == year && x.Date.Month == month;
        Expression<Func<Expense, bool>> expenseExpression = x => x.Date.Year == year && x.Date.Month == month;
        ResponseDto<SummaryDto> response = new();

        var receitas = await _revenueRepository.GetAllAsync(revenueExpression);
        var despesas = await _expenseRepository.GetAllAsync(expenseExpression);

        var totalRevenueMonth = receitas.Sum(x => x.Value);
        var totalExpenseMonth = despesas.Sum(x => x.Value);
        var categoryAmount = despesas.GroupBy(x => x.Category).Select(x => new ValueCategoryDto { Category = x.Key, Value = (double)Math.Round(x.Sum(s => s.Value)) });

        response.Data = new SummaryDto
        {
            TotalRevenueMonth = (double)Math.Round(totalRevenueMonth, 2),
            TotalExpenseMonth = (double)Math.Round(totalExpenseMonth, 2),
            EndingBalanceMonth = (double)Math.Round(totalRevenueMonth - totalExpenseMonth, 2),
            EndingBalanceMonthCategory = categoryAmount
        };

        return response;
    }
}
