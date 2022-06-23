using Blog.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Blog.Infrastructure.Services;

public class EmailService : IEmailService
{
    #region Fields :
    private readonly SmtpSettings _settings;
    private readonly SmtpClient _client;
    private readonly ILogger<EmailService> _logger;
    #endregion

    #region CTORS :
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
    #endregion

    #region Methods :
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage(_settings.From, email, subject, body);
            await _client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email");
        }
    }
    #endregion
}
