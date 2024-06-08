﻿namespace BloodBankManager.Infrastructure.EmailConfig;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
