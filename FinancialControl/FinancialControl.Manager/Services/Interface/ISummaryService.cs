using FinancialControl.Core.Shared.Dtos;

namespace FinancialControl.Manager.Services.Interface
{
    public interface ISummaryService
    {
        Task<ResponseDto<SummaryDto>> GetSummary(int year, int month);
    }
}
