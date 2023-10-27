using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Core.Models.User;

public class ConfirmEmailViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}