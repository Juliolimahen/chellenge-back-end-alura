using FinancialControl.Dtos;
using FinancialControl.Models;
using System;
using Xunit;

namespace FinancialControlTests
{
    public class ExpenseTests
    {
        [Fact]
        public void TesteCriarDespesaValida()
        {
            //Arrange
            //Act 
            var expense = new ExpenseDto()
            {
                Description = "Compras",
                Date = new DateTime(2022, 08, 20),
                Value = 500,
                Category = Category.Alimentacao
            };

            //Assert 
            Assert.NotNull(expense.Description);
            Assert.NotNull(expense.Date);
            Assert.NotNull(expense.Value);
        }
    }
}