using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Summary;

namespace FinancialControl.Manager.Services.Interface
{
    public interface ISummaryService
    {
        Task<ResponseDto<SummaryDto>> GetSummary(int year, int month);
    }
}
