using Microsoft.AspNetCore.Identity;

namespace FinancialControl.Core.Models.User;

public class ApplicationUser : IdentityUser
{
    public string EmailConfirmationToken { get; set; }
    public DateTime? EmailConfirmationTokenExpiresAt { get; set; }
    public bool IsEmailConfirmed { get; set; }
}
