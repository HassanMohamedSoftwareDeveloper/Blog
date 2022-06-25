using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Write;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Helpers;

public static class SeedDataHelper
{
    public static async Task SeedAdmin(IServiceProvider serviceProvider)
    {
        try
        {
            var scope = serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");

            if (ctx.Roles.Any() is false)
            {
                await roleManager.CreateAsync(adminRole);
            }
            if (ctx.Users.Any(u => u.UserName == "admin") is false)
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                    FirstName = "Hassan",
                    LastName = "Mohamed",
                    Image = "avatar.jpg"
                };
                await userManager.CreateAsync(adminUser, "P@ssw0rd");
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
