using FinancialControl.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Manager.Services.Interface
{
    public interface ITokenService
    {
        Token CreateToken(IdentityUser<int> usuario);
    }
}