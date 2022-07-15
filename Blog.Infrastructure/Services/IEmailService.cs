namespace Blog.Infrastructure.Services;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
    Task SendVerificationEmailAsync(string email, string subject, string body);
}
