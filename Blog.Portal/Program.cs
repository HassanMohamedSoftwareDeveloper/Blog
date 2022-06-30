using Blog.Infrastructure;
using Blog.Infrastructure.Helpers;
using Blog.Middlewares;
using Blog.Portal.Validations;
using Microsoft.AspNetCore.Authentication.Cookies;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorPages();
    builder.Services.AddControllers(options =>
    {
        options.EnableEndpointRouting = false;
        options.CacheProfiles.Add("Monthly", new Microsoft.AspNetCore.Mvc.CacheProfile
        {
            Duration = 60 * 60 * 24 * 7 * 4
        });
    });

    builder.Services.AddInfrasturcture(builder.Configuration);
    builder.Services.AddValidators();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Denied";
    });

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();

    var app = builder.Build();
    app.UseMiddleware<ExceptionHandler>();
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseDeveloperExceptionPage();
    //}
    if (!app.Environment.IsDevelopment())
    {
        //app.UseStatusCodePagesWithRedirects("/Error{0}");
        //app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();

    app.UseAuthorization();
    app.MapDefaultControllerRoute();
    app.MapRazorPages();
    await SeedDataHelper.SeedAdmin(app.Services);
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}


