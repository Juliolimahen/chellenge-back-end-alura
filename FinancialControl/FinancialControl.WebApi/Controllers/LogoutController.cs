using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogoutController : ControllerBase
{
    private readonly ILogoutService _logoutService;

    public LogoutController(ILogoutService logoutService)
    {
        _logoutService = logoutService;
    }

    [HttpPost]
    public async Task<IActionResult> LogoutUser()
    {
        try
        {
            IdentityResult resultado = await _logoutService.LogoutUser();
            return resultado.Succeeded
                ? Ok("Logout successful.")
                : BadRequest(
                    new ResponseDto<LoginRequest>
                    {
                        Success = false,
                        Erros = (List<string>)resultado.Errors
                    });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro durante o logout: " + ex.Message);
        }
    }
}
