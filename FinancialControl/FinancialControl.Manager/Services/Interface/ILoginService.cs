using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos.Requests;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services.Interface;

public interface ILoginService
{
    Task<LoggedUser> UserLoginAsync(LoginRequest request);
}
