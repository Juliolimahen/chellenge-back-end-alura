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
        LoggedUser userToken = await _loginService.UserLoginAsync(request);

        return userToken is not null
            ? Ok(userToken)
            : Unauthorized(new ResponseDto<ExpenseDto>
            {
                Success = false,
                Erros = new List<string> { "Invalid login credentials." }
            });
    }
}
