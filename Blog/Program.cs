using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["DefaultConnetion"]));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IFileManager, FileManager>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseMvcWithDefaultRoute();


await SeedAdmin(app.Services);

app.Run();



static async Task SeedAdmin(IServiceProvider serviceProvider)
{
    try
    {
        var scope = serviceProvider.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        ctx.Database.EnsureCreated();
        var adminRole = new IdentityRole("Admin");

        if (ctx.Roles.Any() is false)
        {
            await roleManager.CreateAsync(adminRole);
        }
        if (ctx.Users.Any(u => u.UserName=="admin") is false)
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@test.com",
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

/*
 app.MapGet("/", () => "Hello World!");
*/