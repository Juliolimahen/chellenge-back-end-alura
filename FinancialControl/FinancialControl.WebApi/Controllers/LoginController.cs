using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Core.Shared.Dtos;
using FinancialControl.Core.Shared.Dtos.Requests;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var userToken = await _loginService.UserLoginAsync(request);

        if (userToken != null)
        {
            return Ok(userToken.Value);
        }
        else
        {
            return Unauthorized(new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string> { "Login inválido." }
            });
        }
    }
}
