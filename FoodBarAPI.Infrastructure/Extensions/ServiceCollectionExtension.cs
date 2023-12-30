using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Infrastructure.Persistence;
using FoodBarAPI.Infrastructure.Repositories;
using FoodBarAPI.Infrastructure.Seeders;

namespace FoodBarAPI.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("FoodBarAPI")
        ));

        services.AddScoped<ProductSeeder>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}