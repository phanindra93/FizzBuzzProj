using FizzBuzzProj.Interfaces;
using FizzBuzzProj.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDivisibleByThree, DivisibleByThree>();
builder.Services.AddScoped<IDivisibleByFive, DivisibleByFive>();
builder.Services.AddScoped<IDivisibleByThreeAndFive, DivisibleByThreeAndFive>();

var app = builder.Build();

// Configure the HTTP request pipeline using Startup class.
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
