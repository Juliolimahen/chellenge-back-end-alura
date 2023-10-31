using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Shared.Dtos.Requests;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
