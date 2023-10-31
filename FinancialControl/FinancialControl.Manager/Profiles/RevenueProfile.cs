using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos.Revenue;

namespace FinancialControl.WebApi.Profiles;

public class RevenueProfile : Profile
{
    public RevenueProfile()
    {
        CreateMap<Revenue, RevenueDto>().ReverseMap();
        CreateMap<Revenue, CreateRevenueDto>().ReverseMap();
    }
}
