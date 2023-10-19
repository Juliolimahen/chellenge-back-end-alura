using FinancialControl.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialControl.Core.Shared.Dtos.Expense;

public class CreateExpenseDto
{
    public CreateExpenseDto(string? description, decimal value, DateTime date, Category? category)
    {
        Description = description;
        Value = value;
        Date = date;
        Category = category;
    }

    public CreateExpenseDto(int id, string? description, decimal value, DateTime date, Category? category)
    {
        Id = id;
        Description = description;
        Value = value;
        Date = date;
        Category = category;
    }

    public CreateExpenseDto()
    {
    }

    [JsonIgnore]
    public int Id { get; set; }
    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Value is Required")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The Date is Required")]
    public DateTime Date { get; set; }
    public Category? Category { get; set; }
}
