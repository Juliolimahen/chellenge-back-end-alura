using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Core.Shared.Dtos.Summary;

public class SummaryDto
{
    [Precision(20, 2)]
    public double TotalRevenueMonth { get; set; }
    [Precision(20, 2)]
    public double TotalExpenseMonth { get; set; }
    [Precision(20, 2)]
    public double EndingBalanceMonth { get; set; }
    public IEnumerable<ValueCategoryDto>? EndingBalanceMonthCategory { get; set; }
}
