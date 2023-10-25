using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Shared.Dtos.Revenue;

public class RevenueDto : ICloneable
{
    public RevenueDto(int id, string? description, decimal value, DateTime date)
    {
        Id = id;
        Description = description;
        Value = value;
        Date = date;
    }

    public RevenueDto(){}

    public RevenueDto(string? description, decimal value, DateTime date)
    {
        Description = description;
        Value = value;
        Date = date;
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Value is Required")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The Date is Required")]
    public DateTime Date { get; set; }

    public object Clone()
    {
        var revenue = (RevenueDto)MemberwiseClone();
        return revenue;
    }

    public RevenueDto CloneTipado()
    {
        return (RevenueDto)Clone();
    }
}
