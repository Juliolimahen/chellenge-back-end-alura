using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Models.User;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Core.Shared.Dtos.Revenue;

namespace FinancialControl.WebApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Revenue, RevenueDto>().ReverseMap();
        CreateMap<Expense, ExpenseDto>().ReverseMap();
        CreateMap<Revenue, CreateRevenueDto>().ReverseMap();
        CreateMap<Expense, CreateExpenseDto>().ReverseMap();
        CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
        CreateMap<User, RegisterViewModel>().ReverseMap();
        CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
       CreateMap<ApplicationUser, ConfirmEmailViewModel>().ReverseMap();
    }
}
