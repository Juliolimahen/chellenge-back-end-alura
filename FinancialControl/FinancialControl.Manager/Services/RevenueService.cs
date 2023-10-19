using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Data.Repositories.Interface;
using FinancialControl.Manager.Services.Interface;

namespace FinancialControl.Manager.Services;

public class RevenueService : IRevenueService
{
    private readonly IRevenueRepository _revenueRepository;
    private readonly IMapper _mapper;

    public RevenueService(IRevenueRepository revenueRepository, IMapper mapper)
    {
        _revenueRepository = revenueRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenues(string? description)
    {
        ResponseDto<IEnumerable<RevenueDto>> response = new();

        IEnumerable<Revenue> revenues = !string.IsNullOrEmpty(description)
            ? await _revenueRepository.GetAll(x => x.Description.Contains(description))
            : await _revenueRepository.GetAll();
        response.Data = _mapper.Map<IEnumerable<RevenueDto>>(revenues);
        return response;
    }

    public async Task<RevenueDto> GetRevenueById(int id)
    {
        var revenueEntity = await _revenueRepository.GetById(id);
        return _mapper.Map<RevenueDto>(revenueEntity);
    }

    public async Task<ResponseDto<RevenueDto>> CreateRevenue(CreateRevenueDto revenueDto)
    {
        ResponseDto<RevenueDto> response = new();
        #region Query validation month
        var exists = await _revenueRepository.FirstOrDefaultAsync(
            x => x.Description == revenueDto.Description
            && x.Date.Month == revenueDto.Date.Month
            && x.Date.Year == revenueDto.Date.Year);

        if (exists is not null)
        {
            response.Success = false;
            response.Erros.Add($"there is already an expense with the description {revenueDto.Description} for the date {revenueDto.Date.Month}/{revenueDto.Date.Year}");
            return response;
        }
        #endregion

        var revenueEntity = _mapper.Map<Revenue>(revenueDto);
        await _revenueRepository.Create(revenueEntity);
        response.Data = _mapper.Map<RevenueDto>(revenueEntity);
        //revenueDto.Id = revenueEntity.Id;
        return response;
    }

    public async Task<ResponseDto<RevenueDto>> UpdateRevenue(RevenueDto revenueDto)
    {
        ResponseDto<RevenueDto> response = new();
        #region Query validation month
        var exists = await _revenueRepository.FirstOrDefaultAsync(
            x => x.Description == revenueDto.Description
            && x.Date.Month == revenueDto.Date.Month
            && x.Date.Year == revenueDto.Date.Year);

        if (exists is not null)
        {
            response.Success = false;
            response.Erros.Add($"there is already an expense with the description {revenueDto.Description} for the date {revenueDto.Date.Month}/{revenueDto.Date.Year}");
            return response;
        }
        #endregion

        var revenueEntity = _mapper.Map<Revenue>(revenueDto);
        await _revenueRepository.Update(revenueEntity);
        return response;
    }

    public async Task DeleteRevenue(int id)
    {
        var revenueEntity = _revenueRepository.GetById(id).Result;
        await _revenueRepository.Delete(revenueEntity.Id);
    }

    public async Task<ResponseDto<IEnumerable<RevenueDto>>> GetRevenueByDate(string year, string month)
    {
        ResponseDto<IEnumerable<RevenueDto>> response = new();

        var revenues = await _revenueRepository.GetAll(x => x.Date.Year.ToString() == year && x.Date.Month.ToString() == month);

        if (!revenues.Any())
        {
            response.Success = false;
            response.Erros.Add($"No expenses found on this date {month}/{year}");
            return response;
        }

        response.Data = _mapper.Map<IEnumerable<RevenueDto>>(revenues);
        return response;
    }
}
