using Blog.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Installers;

public class GoogleAuthInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var googleAuthSettings = new GoogleAuthSettings();
        configuration.Bind(nameof(GoogleAuthSettings), googleAuthSettings);
        services.AddSingleton(googleAuthSettings);


    }
}
