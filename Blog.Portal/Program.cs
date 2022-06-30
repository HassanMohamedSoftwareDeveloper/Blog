using Blog.Infrastructure;
using Blog.Infrastructure.Helpers;
using Blog.Portal.Validations;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//options =>
//{
//    options.Conventions.AddPageRoute("/Dashboard/Index", "");
//}
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddInfrasturcture(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/Denied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();
await SeedDataHelper.SeedAdmin(app.Services);
app.Run();


