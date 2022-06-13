using Blog.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Blog.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    private readonly SmtpClient _client;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<SmtpSettings> options, ILogger<EmailService> logger)
    {
        _settings = options.Value;
        _client = new SmtpClient(_settings.Server, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = true,
            UseDefaultCredentials = false
        };
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage(_settings.From, email, subject, body);
            await _client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            _logger.LogInformation(ex.Message);
        }
    }
}
