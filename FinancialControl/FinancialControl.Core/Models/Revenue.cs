using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialControl.Core.Models;

/// <summary>
/// Classe responsável por modelar as receitas
/// </summary>
public class Revenue : Entity
{
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}
