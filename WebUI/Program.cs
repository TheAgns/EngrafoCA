using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebUI.Common.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration);

// Adds exception handling to all controllers
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ProblemDetailsFactory, EngrafoCAProblemDetailsFactory>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// Adds a middleware that catches the exception and logs it,
// then resets the request path en re-execute it to the declared path ("/error)
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
