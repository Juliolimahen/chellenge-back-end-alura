using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Shared.Dtos.User;

public class CreateUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
