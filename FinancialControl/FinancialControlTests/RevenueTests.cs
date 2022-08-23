using FinancialControl.Dtos;
using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancialControlUnitTests
{
    public class RevenueTests
    {
        [Fact]
        public void CriarReceitaValida()
        {
            //Arrange
            //Act
            var revenue = new RevenueDto()
            {
                Description = "Salário",
                Date = new DateTime(2022, 08, 02),
                Value = 2500.67
            };

            //Assert
            Assert.NotNull(revenue.Description);
            Assert.NotNull(revenue.Value);
            Assert.NotNull(revenue.Date);
        }
    }
}
