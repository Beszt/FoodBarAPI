using FoodBarAPI.Infrastructure.Extensions;
using FoodBarAPI.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Populate database with seed data.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodBar API");
});

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();