using AutoMapper;
using FinancialControl.Dtos;
using FinancialControl.Models;

namespace FinancialControl.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Revenue, RevenueDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Revenue, CreateRevenueDto>().ReverseMap();
            CreateMap<Expense, CreateExpenseDto>().ReverseMap();
        }
    }
}
