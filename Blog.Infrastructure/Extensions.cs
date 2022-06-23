using Blog.Application.Services;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrasturcture(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddPersistence(configuration);
        @this.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
        @this.AddSingleton<IEmailService, EmailService>();
        @this.AddScoped<IUserManagerService, UserManagerService>();
        @this.AddAutoMapper(Assembly.GetExecutingAssembly());
        return @this;
    }
}
