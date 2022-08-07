namespace FinancialControl.Models
{
    /// <summary>
    /// Classe responsável por modelar as receitas
    /// </summary>
    public class Revenue
    {
        public int Id { get; set; }
        public string? Descripition { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public Revenue(int id, string descripition, double value, DateTime date)
        {
            Id = id;
            Descripition = descripition;
            Value = value;
            Date = date;
        }

        public Revenue()
        {
        }
    }
}
