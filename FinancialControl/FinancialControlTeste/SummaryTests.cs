using FinancialControl.Dtos;
using FinancialControl.Models;
using FinancialControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancialControlTestsDomain
{
    public class SummaryTests
    {
        //private readonly SummaryService _summaryService;

        //public SummaryTests(SummaryService summaryService)
        //{
        //    _summaryService = summaryService;
        //}

        [Fact]
        public void CalculoSaldoMes()
        {
            //Arrange 
            var receita = new Revenue()
            {

                Id = 1,
                Date = DateTime.Now,
                Description = "salario",
                Value = 2000

            };

            var despesa = new Expense()
            {
                Id = 1,
                Description = "Compras",
                Value = 10000000000000000000,
                Date = DateTime.Now,
                Category = Category.Alimentacao,
            };

            //Act
            var summary = new SummaryDto();
            //Asser
        }
    }
}
