using Microsoft.Extensions.Logging;
using TodoList.Core.Interfaces;

namespace TodoList.Infrastructure.Email;

public class FakeEmailSender(ILogger<FakeEmailSender> _logger) : IEmailSender
{
    public Task SendEmailAsync(string to, string from, string subject, string body)
    {
        _logger.LogInformation("Not actually sending an email to {To} from {From} with subject {Subject}", to, from,
            subject);
        return Task.CompletedTask;
    }
}
