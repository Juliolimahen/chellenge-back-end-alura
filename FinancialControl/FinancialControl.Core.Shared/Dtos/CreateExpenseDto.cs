﻿using FinancialControl.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialControl.Core.Shared.Dtos;

public class CreateExpenseDto
{
    [JsonIgnore]
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
}
