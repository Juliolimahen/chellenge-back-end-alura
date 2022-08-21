using FinancialControl.Dtos;
using FinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancialControlTestsDomain
{
    public class ReceitasTestes
    {
        private const string? DESCRICAO = "Salário";
        private  DateTime DATA = new DateTime(2022, 08, 01);
        private const double VALOR = 2500.78;

        [Fact]
        public void CriarReceitaValida()
        {
            //Arrange
            string Descricao = "Salário";
            double Valor = 2500.78;
            DateTime Data = new DateTime(2022, 08, 01);

            //Act
            var receita = new RevenueDto()
            {
                Description = Descricao,
                Date = Data,
                Value = Valor,
            };

            //Assert
            Assert.Equal(DESCRICAO, receita.Description);
            Assert.Equal(VALOR, receita.Value);
            Assert.Equal(DATA, receita.Date);
        }

        [Fact]
        public void TestaValidacaoReceitas()
        {
            //Arrange
            string Descricao = "Salário";
            double Valor = 2500.78;
            DateTime Data = new DateTime(2022, 08, 01);

            //Act
            var receita = new Revenue()
            {
            };

            //Assert
            //Assert.Throws<Exception>();
        }
    }
}
