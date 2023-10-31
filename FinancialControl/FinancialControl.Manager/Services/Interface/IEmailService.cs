namespace FinancialControl.Manager.Services.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string[] destinatario, string assunto, int usuarioId, string code);
}
