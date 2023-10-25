using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Revenue;
using FinancialControl.Data.Repositories.Interface;
using FinancialControl.Manager.Services.Interface;

namespace FinancialControl.Manager.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepository _revenueRepository;
        private readonly IMapper _mapper;

        public RevenueService(IRevenueRepository revenueRepository, IMapper mapper)
        {
            _revenueRepository = revenueRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenuesAsync(string? description)
        {
            var response = new ResponseDto<IEnumerable<RevenueDto>>();

            IEnumerable<Revenue> revenues = string.IsNullOrEmpty(description)
                ? await _revenueRepository.GetAllAsync()
                : await _revenueRepository.GetAllAsync(x => x.Description.Contains(description));

            response.Data = _mapper.Map<IEnumerable<RevenueDto>>(revenues);
            return response;
        }

        public async Task<ResponseDto<RevenueDto>> GetRevenueByIdAsync(int id)
        {
            var revenueEntity = await _revenueRepository.GetByIdAsync(id);
            return _mapper.Map<ResponseDto<RevenueDto>>(revenueEntity);
        }

        public async Task<ResponseDto<RevenueDto>> CreateRevenueAsync(CreateRevenueDto revenueDto)
        {
            var response = new ResponseDto<RevenueDto>();

            var exists = await _revenueRepository.FirstOrDefaultAsync(
                x => x.Description == revenueDto.Description
                && x.Date.Month == revenueDto.Date.Month
                && x.Date.Year == revenueDto.Date.Year);

            if (exists != null)
            {
                response.Success = false;
                response.Erros.Add($"There is already a revenue with the description {revenueDto.Description} for the date {revenueDto.Date.Month}/{revenueDto.Date.Year}");
                return response;
            }

            var revenueEntity = _mapper.Map<Revenue>(revenueDto);
            await _revenueRepository.CreateAsync(revenueEntity);
            response.Data = _mapper.Map<RevenueDto>(revenueEntity);
            return response;
        }

        public async Task<ResponseDto<RevenueDto>> UpdateRevenueAsync(RevenueDto revenueDto)
        {
            var response = new ResponseDto<RevenueDto>();

            var exists = await _revenueRepository.FirstOrDefaultAsync(
                x => x.Description == revenueDto.Description
                && x.Date.Month == revenueDto.Date.Month
                && x.Date.Year == revenueDto.Date.Year);

            if (exists != null)
            {
                response.Success = false;
                response.Erros.Add($"There is already a revenue with the description {revenueDto.Description} for the date {revenueDto.Date.Month}/{revenueDto.Date.Year}");
                return response;
            }

            var revenueEntity = _mapper.Map<Revenue>(revenueDto);
            await _revenueRepository.UpdateAsync(revenueEntity);
            return response;
        }

        public async Task DeleteRevenueAsync(int id)
        {
            var revenueEntity = await _revenueRepository.GetByIdAsync(id);
            await _revenueRepository.DeleteAsync(revenueEntity.Id);
        }

        public async Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenueByDateAsync(string year, string month)
        {
            var response = new ResponseDto<IEnumerable<RevenueDto>>();

            var revenues = await _revenueRepository.GetAllAsync(x =>
                x.Date.Year.ToString() == year && x.Date.Month.ToString() == month);

            if (!revenues.Any())
            {
                response.Success = false;
                response.Erros.Add($"No revenues found on this date {month}/{year}");
            }
            else
            {
                response.Data = _mapper.Map<IEnumerable<RevenueDto>>(revenues);
            }

            return response;
        }
    }
}
