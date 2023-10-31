using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Core.Shared.Dtos.User;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{

    private readonly IRegisterService _registerService;

    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUserDto createDto)
    {
        IdentityResult resultado = await _registerService.RegisterUserAsync(createDto);
        if (resultado.Succeeded)
        {
            return Ok("User registered successfully.");
        }
        else
        {
            return BadRequest(resultado.Errors);
        }
    }

    [HttpGet("ativa")]
    public async Task<IActionResult> AtivaContaUsuario([FromQuery] ActivateAccountRequest request)
    {
        IdentityResult resultado = await _registerService.ActivateUserAccount(request);
        if (resultado.Succeeded)
        {
            return Ok("User account activated successfully.");
        }
        else
        {
            return BadRequest(resultado.Errors);
        }
    }
}
