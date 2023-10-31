using AutoMapper;
using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos.Expense;

namespace FinancialControl.WebApi.Profiles;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<Expense, ExpenseDto>().ReverseMap();
        CreateMap<Expense, CreateExpenseDto>().ReverseMap();
    }
}
