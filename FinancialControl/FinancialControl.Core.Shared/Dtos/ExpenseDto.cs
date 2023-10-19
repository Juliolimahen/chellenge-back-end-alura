using FinancialControl.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Shared.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Value is Required")]
        public double Value { get; set; }

        [Required(ErrorMessage = "The Date is Required")]
        public DateTime Date { get; set; }
        public Category? Category { get; set; }

        public ExpenseDto(int id, string? description, double value, DateTime date, Category? category)
        {
            Id = id;
            Description = description;
            Value = value;
            Date = date;
            Category = category;
        }

        public ExpenseDto(int id)
        {
            Id = id;
        }

        public ExpenseDto()
        {

        }
    }
}
