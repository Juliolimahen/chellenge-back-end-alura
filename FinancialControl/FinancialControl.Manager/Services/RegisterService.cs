using AutoMapper;
using FinancialControl.Core.Models.User;
using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Core.Shared.Dtos.User;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace FinancialControl.Manager.Services;

public class RegisterService : IRegisterService
{
    private IMapper _mapper;
    private UserManager<IdentityUser<int>> _userManager;
    private IEmailService _emailService;

    public RegisterService(IMapper mapper,
        UserManager<IdentityUser<int>> userManager,
        IEmailService emailService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<IdentityResult> RegisterUserAsync(CreateUserDto createDto)
    {
        User user = _mapper.Map<User>(createDto);
        IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
        var resultadoIdentity = await _userManager.CreateAsync(userIdentity, createDto.Password);

        if (resultadoIdentity.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userIdentity);
            var encodedCode = UrlEncoder.Default.Encode(code);

            await _emailService.SendEmailAsync(new[] { userIdentity.Email },
                 "Link de Ativação", userIdentity.Id, code);

            return resultadoIdentity;
        }

        return resultadoIdentity;
    }

    public async Task<IdentityResult> ActivateUserAccount(ActivateAccountRequest request)
    {
        var identityUser = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (identityUser != null)
        {
            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, request.ActivationCode);
            return identityResult;
        }

        return IdentityResult.Failed(new IdentityError { Description = "Falha ao ativar conta de usuário" });
    }
}
