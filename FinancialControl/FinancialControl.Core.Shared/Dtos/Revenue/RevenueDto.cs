using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Shared.Dtos.Revenue;

public class RevenueDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Value is Required")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The Date is Required")]
    public DateTime Date { get; set; }
}
