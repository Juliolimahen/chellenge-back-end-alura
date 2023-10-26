using FinancialControl.Core.Models.User;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Core.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {
            var result = await _authService.CreateUser(model);

            if (result.Succeeded)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Usuário ou senha inválidos");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo)
        {
            var userToken = await _authService.Login(userInfo);

            if (userToken != null)
            {
                return Ok(userToken);
            }
            else
            {
                return BadRequest(new ResponseDto<ExpenseDto>
                {
                    Success = false,
                    Erros = new List<string> { "Login inválido." }
                });
            }
        }
    }
}
