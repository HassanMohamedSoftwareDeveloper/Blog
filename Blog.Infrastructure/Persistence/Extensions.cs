using Blog.Application.Services;
using Blog.Domain.Repositories;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Repositories;
using Blog.Infrastructure.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Infrastructure.Persistence;

internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddScoped<IPostRepository, PostRepository>();
        @this.AddScoped<ICategoryRepository, CategoryRepository>();

        @this.AddScoped<ICategoryReadService, CategoryReadService>();


        var connectionString = configuration["DefaultConnetion"];

        /*UseLazyLoadingProxies().*/
        void optionsBuilder(DbContextOptionsBuilder options) => options.UseSqlServer(connectionString);

        @this.AddDbContext<WriteDbContext>(optionsBuilder);
        @this.AddDbContext<ReadDbContext>(optionsBuilder);

        @this.AddMediatR(Assembly.Load("Blog.Application"));
        @this.AddMediatR(Assembly.GetExecutingAssembly());


        return @this;
    }
}
