using Blog.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Mail;

namespace Blog.Infrastructure.Services;

public class EmailService : IEmailService
{
    #region Fields :
    private readonly SmtpSettings _settings;
    private readonly SmtpClient _client;
    private readonly MailGunEmailConfig _emailOptions;
    private readonly ILogger<EmailService> _logger;
    private readonly HttpClient _httpClient;
    #endregion

    #region CTORS :
    public EmailService(IOptions<MailGunEmailConfig> emailOptions, IOptions<SmtpSettings> options, ILogger<EmailService> logger, HttpClient httpClient)
    {
        _settings = options.Value;
        _client = new SmtpClient(_settings.Server, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = true,
            UseDefaultCredentials = false
        };
        _emailOptions = emailOptions.Value;
        _logger = logger;
        _httpClient = httpClient;
    }
    #endregion

    #region Methods :
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage(_settings.From, email, subject, body)
            {
                IsBodyHtml = true
            };
            await _client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email");
        }
    }

    public async Task SendVerificationEmailAsync(string email, string subject, string body)
    {
        RestClient client = new(_emailOptions.URL)
        {
            Authenticator = new HttpBasicAuthenticator(_emailOptions.User_Name, _emailOptions.API_KEY)
        };

        RestRequest request = new("", Method.Post);
        request.AddParameter("domain", _emailOptions.Domain, ParameterType.UrlSegment);
        request.Resource = _emailOptions.Resource;
        request.AddParameter("from", _emailOptions.From);
        request.AddParameter("to", email);
        request.AddParameter("subject", subject);
        request.AddParameter("text", body);
        var result = await client.ExecuteAsync(request);
    }
    #endregion
}
