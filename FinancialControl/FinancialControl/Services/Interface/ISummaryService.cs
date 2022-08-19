using FinancialControl.Dtos;

namespace FinancialControl.Services.Interface
{
    public interface ISummaryService
    {
        Task<ResponseDto<SummaryDto>> GetSummary(int year, int month);
    }
}
