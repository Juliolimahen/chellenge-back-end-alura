using FinancialControl.Models;
using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Dtos
{
    public class CreateExpenseDto
    {
        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Value is Required")]
        public double Value { get; set; }

        [Required(ErrorMessage = "The Date is Required")]
        public DateTime Date { get; set; }
        public Category? Category { get; set; }
    }
}
