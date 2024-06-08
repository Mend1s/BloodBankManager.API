using System.Net;
using System.Net.Mail;

namespace BloodBankManager.Infrastructure.EmailConfig;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    public EmailService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("", ""),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage("", toEmail)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
            Console.WriteLine("E-mail enviado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}
