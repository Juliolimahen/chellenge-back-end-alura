using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Core.Shared.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services.Interface;

public interface IRegisterService
{
    Task<IdentityResult> ActivateUserAccount(ActivateAccountRequest request);
    Task<IdentityResult> RegisterUserAsync(CreateUserDto createDto);
}
