using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UppsalaBlazor.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<Database>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
});

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
