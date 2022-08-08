using FinancialControl.Dtos;

namespace FinancialControl.Services
{
    public interface IRevenueService
    {
        Task<IEnumerable<RevenueDto>> GetRevenues();
        Task<RevenueDto> GetRevenueById(int id);
        Task<ResponseDto<RevenueDto>> CreateRevenue(CreateRevenueDto revenueDto);
        Task <ResponseDto<RevenueDto>>UpdateRevenue(RevenueDto revenueDto);
        Task DeleteRevenue(int id);
    }
}
