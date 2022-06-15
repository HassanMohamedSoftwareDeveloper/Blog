using Blog.Configuration;
using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Middlewares;
using Blog.Models;
using Blog.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection(nameof(SmtpSettings)));

builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration["DefaultConnetion"]));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.CacheProfiles.Add("Monthly", new Microsoft.AspNetCore.Mvc.CacheProfile
    {
        Duration = 60 * 60 * 24 * 7 * 4
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
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

/*
 app.MapGet("/", () => "Hello World!");
*/