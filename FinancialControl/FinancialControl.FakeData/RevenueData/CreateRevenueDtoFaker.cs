using Bogus;
using FinancialControl.Core.Shared.Dtos.Revenue;

namespace FinancialControl.FakeData.RevenueData;

public class CreateRevenueDtoFaker : Faker<CreateRevenueDto>
{
    public CreateRevenueDtoFaker()
    {
        RuleFor(e => e.Description, f => f.Lorem.Sentence(3)); // Gerar descrição fictícia
        RuleFor(e => e.Value, f => f.Finance.Amount()); // Gerar um valor fictício
    }
}
