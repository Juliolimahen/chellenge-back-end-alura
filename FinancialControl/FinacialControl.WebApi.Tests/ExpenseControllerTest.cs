using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.FakeData.ExpenseData;
using FinancialControl.Manager.Services.Interface;
using FinancialControl.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FinacialControl.WebApi.Tests;

public class ExpenseControllerTest
{
    private readonly List<ExpenseDto> expenses;
    private readonly ExpenseController controller;
    private readonly IExpenseService expenseService;
    private readonly CreateExpenseDto createExpenseDto;
    private readonly ExpenseDto expenseDto;

    public ExpenseControllerTest()
    {

        expenseService = Substitute.For<IExpenseService>();
        controller = new ExpenseController(expenseService);
        expenseDto = new ExpenseDtoFaker().Generate();
        expenses = new ExpenseDtoFaker().Generate(10);
        createExpenseDto = new CreateExpenseDtoFaker().Generate();
    }

    [Fact]
    public async Task GetAllExpenses_Ok()
    {
    }

    [Fact]
    public async Task PostExpenses_Created()
    {
    }
}
