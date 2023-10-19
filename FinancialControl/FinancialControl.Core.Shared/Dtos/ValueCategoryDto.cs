using FinancialControl.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Core.Shared.Dtos;

public class ValueCategoryDto
{
    public Category Category { get; set; }

    [Precision(20, 2)]
    public double Value { get; set; }
}
