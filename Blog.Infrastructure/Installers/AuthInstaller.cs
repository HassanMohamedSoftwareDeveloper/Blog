using Blog.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Installers;

public class AuthInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var googleAuthSettings = new GoogleAuthSettings();
        configuration.Bind(nameof(GoogleAuthSettings), googleAuthSettings);
        var facebookAuthSettings = new FacebookAuthSettings();
        configuration.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddFacebook("facebook", opt =>
            {
                opt.AppId = facebookAuthSettings.AppId;
                opt.AppSecret = facebookAuthSettings.AppSecret;
                opt.SignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddGoogle("google", opt =>
            {
                opt.ClientId = googleAuthSettings.ClientId;
                opt.ClientSecret = googleAuthSettings.ClientSecret;
                opt.SignInScheme = IdentityConstants.ExternalScheme;
            });

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/Denied";
        });
    }
}
