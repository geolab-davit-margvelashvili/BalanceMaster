using BalanceMaster.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BalanceMaster.Api.Extensions;

public static class JwtConfigurationExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            // განსაზღვრავს ჰედერის სქემას რომელშიც უნდა მოძებნოს აუთენთიფიკაციის ტოკენი
            // Authorization: Bearer <TokenValue>
            .AddAuthentication("Bearer")
            // ვამატებთ JWT Bearer ტოკენს, რითიც ვუთითებთ რომ  Authorization ჰედერში
            // Bearer ის შემდეგ ჩაწერილი მნიშვნელობა იქნება JWT ფორმატის ტოკენი
            .AddJwtBearer(options =>
            {
                // ვაკონფიგურირებთ ტოკენის ვალიდაციის პარამეტრებს
                options.TokenValidationParameters = new()
                {
                    // განვსაზღვრავთ ტოკენის რა ველები უნდა დავალიდირდეს
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    // განვსაზღვრავთ იმ მნიშნველობების რის მიმართაც ვალიდირდება ტოკენში გადმოცემული მნიშნველობები
                    ValidIssuer = configuration.GetJwtIssuer(),
                    ValidAudience = configuration.GetJwtAudience(),
                    IssuerSigningKey = configuration.GetIssuerSigningKey(),

                    // ეს პარამეტრი განსაზღვრავს ტოკენის ვადის შემოწმების დროს დასაშვები გადახრა რა დრო შეიძლება რომ იყოს
                    // ამ შემთხვევაში ვანულებთ ამ დროს.
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