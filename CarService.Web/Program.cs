using CarService.Web.Controllers.Loggers;
using CarService.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<CarServices>();
builder.Services.AddScoped<LogFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/exception");
    app.UseStatusCodePagesWithRedirects("~/error/http/{0}");
}

app.MapControllers();
app.UseStaticFiles();
app.Run();
