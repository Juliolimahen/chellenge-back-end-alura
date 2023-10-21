using Bogus;
using FinancialControl.Core.Shared.Dtos.Expense;

namespace FinancialControl.FakeData.ExpenseData;

public class CreateExpenseDtoFaker : Faker<CreateExpenseDto>
{
    public CreateExpenseDtoFaker()
    {
        RuleFor(e => e.Description, f => f.Lorem.Sentence(3)); // Gerar descrição fictícia
        RuleFor(e => e.Value, f => f.Finance.Amount()); // Gerar um valor fictício
    }
}
