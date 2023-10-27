using System.ComponentModel.DataAnnotations;

namespace FinancialControl.WebApi.Controllers;

public class ConfirmEmailViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}