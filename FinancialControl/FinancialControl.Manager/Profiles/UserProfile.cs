using AutoMapper;
using FinancialControl.Core.Models.User;
using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Core.Shared.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.WebApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<User, IdentityUser<int>>().ReverseMap();
        CreateMap<IdentityUser<int>, LoggedUser>().ReverseMap();
        CreateMap<User, LoggedUser>().ReverseMap();
        CreateMap<LoginRequest, LoggedUser>().ReverseMap();
    }
}
