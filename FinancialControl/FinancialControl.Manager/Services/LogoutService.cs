using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services;

public class LogoutService : ILogoutService
{
    private SignInManager<IdentityUser<int>> _signInManager;

    public LogoutService(SignInManager<IdentityUser<int>> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> LogoutUser()
    {
        await _signInManager.SignOutAsync();

        if (_signInManager.Context.User.Identity.IsAuthenticated)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Logout falhou" });
        }

        return IdentityResult.Success;
    }
}
