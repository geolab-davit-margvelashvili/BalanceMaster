using BalanceMaster.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BalanceMaster.Api.Extensions;

public static class JwtConfigurationExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = configuration.GetJwtIssuer(),
                    ValidAudience = configuration.GetJwtAudience(),
                    IssuerSigningKey = configuration.GetIssuerSigningKey(),

                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    public static string GetJwtIssuer(this IConfiguration configuration) =>
        configuration.GetValueOrThrow("Authentication:Issuer");

    public static string GetJwtAudience(this IConfiguration configuration) =>
        configuration.GetValueOrThrow("Authentication:Audience");

    public static string GetJwtSecret(this IConfiguration configuration) =>
        configuration.GetValueOrThrow("Authentication:SecretForKey");

    public static SecurityKey GetIssuerSigningKey(this IConfiguration configuration) =>
        new SymmetricSecurityKey(Convert.FromBase64String(configuration.GetJwtSecret()));

    public static string GetValueOrThrow(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new ConfigurationException($"{key} was not provided in configuration file");
}