using FinancialControl.Core.Models.User;
using FinancialControl.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services.Interface;

public interface IRegistrationService
{
    Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailViewModel model);
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
}