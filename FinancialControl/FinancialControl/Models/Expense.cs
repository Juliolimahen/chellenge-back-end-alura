using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Models
{
    /// <summary>
    /// Classe responsável por modelar as despesas
    /// </summary>
    public class Expense
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public Expense(int id, string description, double value, DateTime date)
        {
            Id = id;
            Description = description;
            Value = value;
            Date = date;
        }

        public Expense()
        {
        }
    }
}
