using FinancialControl.Core.Models.User;
using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<IdentityResult> CreateUser(User model);
    Task<UserToken> Login(User userInfo);
}