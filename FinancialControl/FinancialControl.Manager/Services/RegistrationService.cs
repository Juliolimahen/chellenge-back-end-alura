using FinancialControl.Core.Models.User;
using FinancialControl.Manager.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FinancialControl.Manager.Services;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public RegistrationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

        // Gere o token de confirmação de e-mail
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenExpiresAt = DateTime.UtcNow.AddHours(24); // Expira em 24 horas
        user.IsEmailConfirmed = false;

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await SendEmailConfirmationAsync(user);
        }

        return result;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        if (user.EmailConfirmationToken == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Email already confirmed." });
        }

        return await _userManager.ConfirmEmailAsync(user, model.Token);
    }

    public async Task SendEmailConfirmationAsync(ApplicationUser user)
    {
        var emailConfirmationUrl = GenerateEmailConfirmationLink(user);

        using (SmtpClient client = new SmtpClient())
        {
            var email = new MailMessage
            {
                Subject = "Confirm your email",
                Body = $"Please confirm your email by clicking this link: {emailConfirmationUrl}",
                IsBodyHtml = true,
                From = new MailAddress(_configuration["Smtp:SenderEmail"]),
                BodyEncoding = Encoding.UTF8,
            };

            email.To.Add(user.Email);

            client.Host = _configuration["Smtp:Host"];
            client.Port = int.Parse(_configuration["Smtp:Port"]);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(
                _configuration["Smtp:Username"],
                _configuration["Smtp:Password"]
            );

            try
            {
                await client.SendMailAsync(email);
            }
            catch (Exception ex)
            {
                // Registre o erro usando uma biblioteca de log, se possível.
                // Logger.LogError(ex, "Erro no envio de e-mail de confirmação.");
            }
        }
    }

    private string GenerateEmailConfirmationLink(ApplicationUser user)
    {
        return $"https://localhost:7053/api/Auth/ConfirmEmail?email={user.Email}&token={user.EmailConfirmationToken}";
    }
}
