using FinancialControl.Core.Models;

namespace FinancialControl.Core.Shared.Dtos.Requests;

public class LoggedUser
{
    public string UserName { get; set; }
    public Token Token { get; set; }
}
