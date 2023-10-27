using FinancialControl.Core.Models.User;

public interface IAuthService
{
    Task<UserToken> Login(User userInfo);
}