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
            if (resultado.Succeeded)
            {
                return Ok("Logout successful.");
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro durante o logout: " + ex.Message);
        }
    }
}
