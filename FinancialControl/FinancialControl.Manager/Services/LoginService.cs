using AutoMapper;
using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services;

public class LoginService : ILoginService
{
    private SignInManager<IdentityUser<int>> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public LoginService(SignInManager<IdentityUser<int>> signInManager,
        ITokenService tokenService,
        IMapper mapper
        )
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<LoggedUser> UserLoginAsync(LoginRequest request)
    {
        var resultadoIdentity = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

        if (!resultadoIdentity.Succeeded)
        {
            return null;
        }

        var identityUser = await _signInManager.UserManager.FindByNameAsync(request.UserName);

        if (identityUser == null)
        {
            return null;
        }

        //var userLogado = new LoggedUser();
        var userLogado = _mapper.Map<LoggedUser>(identityUser);
        //userLogado.UserName = identityUser.UserName;
        userLogado.Token = _tokenService.CreateToken(identityUser);

        return userLogado;
    }
}
