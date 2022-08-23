using FinancialControl.Controllers;
using FinancialControl.Dtos;
using FinancialControl.Repositories.Interface;
using FinancialControl.Services;
using FinancialControl.Services.Interface;
using FinancialControlIntregrationTests.Service;
using System.Threading.Tasks;
using Xunit;

namespace FinancialControlIntregrationTests
{
    public class SummaryTests
    {
        private readonly ISummaryService _summaryService;

        public SummaryTests(ISummaryService summaryService, IFinancialControlRepository financialControlRepository)
        {
            _summaryService = summaryService;
        }

        [Fact]
        public async Task TesteCalculoSaldoMes()
        {
            var saldoEperado = 1800;
            var result = await _summaryService.GetSummary(2022, 8);
            Assert.Equal(saldoEperado, result.Data.EndingBalanceMonth);
        }
    }
}