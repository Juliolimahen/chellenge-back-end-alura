﻿using Bogus;
using FinancialControl.Core.Models;

namespace FinancialControl.FakeData.RevenueData;

public class RevenueFaker: Faker<Revenue>
{
	public RevenueFaker()
	{
        var id = new Faker().Random.Number(1, 999999);
        RuleFor(e => e.Id, f => id);
        RuleFor(e => e.Description, f => f.Lorem.Sentence(3)); // Gerar descrição fictícia
        RuleFor(e => e.Value, f => f.Finance.Amount()); // Gerar um valor fictício
    }
}
