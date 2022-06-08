using Blog.Data;
using Blog.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["DefaultConnetion"]));
builder.Services.AddTransient<IRepository, Repository>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();


app.UseRouting();


app.UseMvcWithDefaultRoute();

app.Run();


/*
 app.MapGet("/", () => "Hello World!");
*/