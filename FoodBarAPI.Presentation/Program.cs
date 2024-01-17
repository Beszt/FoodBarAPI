using NLog;
using NLog.Web;
using FoodBarAPI.Application.Extensions;
using FoodBarAPI.Infrastructure.Extensions;
using FoodBarAPI.Infrastructure.Seeders;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Starting up...");

var builder = WebApplication.CreateBuilder(args);

// Add NLog to ASP.NET Core
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

// Populate database with seed data.
var scope = app.Services.CreateScope();
var populator = scope.ServiceProvider.GetRequiredService<IPopulator>();
await populator.Populate();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API v1");
    c.RoutePrefix = "";
});

app.Run();

public partial class Program { }