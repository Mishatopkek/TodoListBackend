﻿using System.Net.Mail;
using Microsoft.Extensions.Logging;
using TodoList.Core.Interfaces;

namespace TodoList.Infrastructure.Email;

public class SmtpEmailSender(ILogger<SmtpEmailSender> _logger) : IEmailSender
{
    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        var emailClient = new SmtpClient("localhost"); // TODO: pull settings from config
        var message = new MailMessage {From = new MailAddress(from), Subject = subject, Body = body};
        message.To.Add(new MailAddress(to));
        await emailClient.SendMailAsync(message);
        _logger.LogWarning("Sending email to {To} from {From} with subject {Subject}.", to, from, subject);
    }
}
