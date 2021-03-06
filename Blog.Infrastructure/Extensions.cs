using Blog.Application.Services;
using Blog.Infrastructure.Installers;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Write;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
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
        @this.Configure<MailGunEmailConfig>(configuration.GetSection(nameof(MailGunEmailConfig)));
        @this.AddScoped<IEmailService, EmailService>();
        @this.AddScoped<IUserManagerService, UserManagerService>();
        @this.AddScoped<IFileManagerService, FileManagerService>();
        @this.AddAutoMapper(Assembly.GetExecutingAssembly());

        new FacebookAuthInstaller().InstallServices(@this, configuration);

        new AuthInstaller().InstallServices(@this, configuration);

        @this.AddIdentity<User, IdentityRole>(opt => opt.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<WriteDbContext>()
             .AddDefaultTokenProviders(); ;

        @this.AddHttpClient();

        return @this;
    }
}
