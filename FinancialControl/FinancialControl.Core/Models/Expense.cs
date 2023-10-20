using FinancialControl.Core.Models.Enums;

namespace FinancialControl.Core.Models;

/// <summary>
/// Classe responsável por modelar as despesas
/// </summary>
public class Expense : Entity
{
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public Category Category { get; set; }
}
