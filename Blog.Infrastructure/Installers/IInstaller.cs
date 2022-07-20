using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Installers;

public interface IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration);
}
