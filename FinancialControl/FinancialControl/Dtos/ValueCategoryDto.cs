using FinancialControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Dtos;

public class ValueCategoryDto
{
    public Category Category { get; set; }

    [Precision(14, 2)]
    public double Value { get; set; }
}
