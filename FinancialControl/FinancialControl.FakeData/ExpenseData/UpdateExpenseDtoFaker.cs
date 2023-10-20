using Bogus;
using FinancialControl.Core.Shared.Dtos.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.FakeData.ExpenseData;

public class UpdateExpenseDtoFaker: Faker<UpdateExpenseDto>
{
	public UpdateExpenseDtoFaker()
	{
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(e => e.Id, f => id);
        RuleFor(e => e.Description, f => f.Lorem.Sentence(3)); // Gerar descrição fictícia
        RuleFor(e => e.Value, f => f.Finance.Amount()); // Gerar um valor fictício
    }
}
