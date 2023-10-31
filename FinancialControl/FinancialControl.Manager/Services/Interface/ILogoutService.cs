using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services.Interface;

public interface ILogoutService
{
    Task<IdentityResult> LogoutUser();
}
