using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
        => _logger = logger;

    public void SendEmail(string to, string message)
        => _logger.LogInformation($"Sending e-mail to {to} with message {message}");
}