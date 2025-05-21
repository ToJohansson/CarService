using CarService.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<CarServices>();

var app = builder.Build();

app.MapControllers();
app.UseStaticFiles();

app.Run();
