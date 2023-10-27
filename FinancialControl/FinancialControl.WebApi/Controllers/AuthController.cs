using FinancialControl.Core.Models.User;
using FinancialControl.Core.Shared.Dtos.Expense;
using FinancialControl.Core.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using FinancialControl.Manager.Services.Interface;

namespace FinancialControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRegistrationService _registrationService;

        public AuthController(IAuthService authService, IRegistrationService registrationService)
        {
            _authService = authService;
            _registrationService = registrationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _registrationService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return Ok("Registration successful. Please check your email to confirm your registration.");
            }
            else
            {
                return BadRequest("Registration failed. Please check the provided information.");
            }
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var result = await _registrationService.ConfirmEmailAsync(model);

            if (result.Succeeded)
            {
                return Ok("Email confirmation successful.");
            }
            else
            {
                return BadRequest("Email confirmation failed.");
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
