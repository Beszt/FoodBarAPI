using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FoodBarAPI.Application.Mappings;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;

namespace FoodBarAPI.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateProductCommand));
        services.AddMediatR(typeof(GetProductQuery));
        services.AddMediatR(typeof(UpdateProductCommand));
        services.AddMediatR(typeof(DeleteProductCommand));

        services.AddScoped(provider => new MapperConfiguration( cfg =>
        {
            cfg.AddProfile(new ProductMappingProfile());
        }).CreateMapper());
    }
}