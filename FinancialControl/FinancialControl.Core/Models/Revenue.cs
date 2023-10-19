﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialControl.Core.Models;

/// <summary>
/// Classe responsável por modelar as receitas
/// </summary>
public class Revenue : Entity
{
    public string? Description { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }

    public Revenue(int id, string description, double value, DateTime date)
    {
        Id = id;
        Description = description;
        Value = value;
        Date = date;
    }
    public Revenue() { }
}
