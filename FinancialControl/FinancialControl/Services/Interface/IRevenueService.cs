using FinancialControl.Dtos;

namespace FinancialControl.Services;

public interface IRevenueService
{
    Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenues(string? description);
    Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenueByDate(string year, string month);
    Task<RevenueDto> GetRevenueById(int id);
    Task<ResponseDto<RevenueDto>> CreateRevenue(CreateRevenueDto revenueDto);
    Task<ResponseDto<RevenueDto>> UpdateRevenue(RevenueDto revenueDto);
    Task DeleteRevenue(int id);
}
