using AutoMapper;
using FinancialControl.Dtos;
using FinancialControl.Models;
using FinancialControl.Repositories.Interface;


namespace FinancialControl.Services;

public class RevenueService : IRevenueService
{
    private readonly IRevenueRepository _revenueRepository;
    private readonly IMapper _mapper;

    public RevenueService(IRevenueRepository revenueRepository, IMapper mapper)
    {
        _revenueRepository = revenueRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RevenueDto>> GetRevenues(string? description)
    {
        IEnumerable<Revenue> revenues;

        if (!string.IsNullOrEmpty(description))
            revenues = await _revenueRepository.GetAll(x => x.Description.Contains(description));
        else
            revenues = await _revenueRepository.GetAll();

        return _mapper.Map<IEnumerable<RevenueDto>>(revenues);
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

    public async Task<IEnumerable<RevenueDto>> GetExpenseByDate(string year, string month)
    {
        var revenues = await _revenueRepository.GetAll(x => x.Date.Year.ToString() == year && x.Date.Month.ToString() == month);
        return _mapper.Map<IEnumerable<RevenueDto>>(revenues);
    }
}
