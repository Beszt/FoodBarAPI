using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FoodBarAPI.Presentation.Settings;

namespace FoodBarAPI.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        var jwt = new JwtSettings();
        configuration.GetSection("Jwt").Bind(jwt);
        services.AddSingleton(jwt);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
            };
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "FoodBarAPI",
                Description = "An ASP.NET Minimap API for obtaining food products info by EAN barcodes.",
                Contact = new OpenApiContact
                {
                    Name = "Author",
                    Email = "Maciej.Obarzanek@gmail.com",
                    Url = new Uri("https://obisoft.pl")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit/")
                }
            });
            c.EnableAnnotations();
        });
    }
}