using System.Text;
using Microsoft.IdentityModel.Tokens;
using FoodBarAPI.Presentation.Settings;

namespace FoodBarAPI.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
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
    }
}