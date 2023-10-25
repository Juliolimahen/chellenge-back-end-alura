using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Revenue;

namespace FinancialControl.Manager.Services.Interface;

public interface IRevenueService
{
    Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenuesAsync(string? description);
    Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenueByDateAsync(string year, string month);
    Task<ResponseDto<RevenueDto>> GetRevenueByIdAsync(int id);
    Task<ResponseDto<RevenueDto>> CreateRevenueAsync(CreateRevenueDto revenueDto);
    Task<ResponseDto<RevenueDto>> UpdateRevenueAsync(RevenueDto revenueDto);
    Task DeleteRevenueAsync(int id);
}
