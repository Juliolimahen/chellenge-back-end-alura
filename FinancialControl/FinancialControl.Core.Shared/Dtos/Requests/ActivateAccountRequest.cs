using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace FinancialControl.Core.Shared.Dtos.Requests
{
    public class ActivateAccountRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ActivationCode { get; set; }
    }
}
