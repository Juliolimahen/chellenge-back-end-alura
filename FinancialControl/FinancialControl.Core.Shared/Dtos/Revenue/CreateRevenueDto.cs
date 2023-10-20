using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialControl.Core.Shared.Dtos.Revenue
{
    public class CreateRevenueDto
    {
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

        public CreateRevenueDto()
        {
        }

        public CreateRevenueDto(int id, string? description, decimal value, DateTime date)
        {
            Id = id;
            Description = description;
            Value = value;
            Date = date;
        }

        public CreateRevenueDto(int id)
        {
            Id = id;
        }
    }
}
