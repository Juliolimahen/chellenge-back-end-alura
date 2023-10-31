using FinancialControl.Core.Models;
using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services;

public class LoginService : ILoginService
{
    private SignInManager<IdentityUser<int>> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginService(SignInManager<IdentityUser<int>> signInManager,
        ITokenService tokenService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Token> UserLoginAsync(LoginRequest request)
    {
        var resultadoIdentity = await _signInManager
            .PasswordSignInAsync(request.Username, request.Password, false, false);

        if (resultadoIdentity.Succeeded)
        {
            var identityUser = await _signInManager.UserManager.FindByNameAsync(request.Username);

            if (identityUser != null)
            {
                Token token = await _tokenService.CreateToken(identityUser);

                return token;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
