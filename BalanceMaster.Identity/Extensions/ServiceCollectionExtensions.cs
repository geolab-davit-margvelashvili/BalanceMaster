using BalanceMaster.Identity.Models;
using BalanceMaster.Identity.Services.Abstractions;
using BalanceMaster.Identity.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BalanceMaster.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IdentityBuilder AddIdentityServices(this IServiceCollection services, IConfiguration configuration, Action<IdentityOptions>? setupAction = null) => services
        .AddScoped<ITokenService, JwtTokenService>()
        .Configure<TokenServiceOptions>(configuration.GetSection(TokenServiceOptions.Key))
        .AddScoped<IIdentityService, IdentityService>()
        .AddIdentity<ApplicationUser, IdentityRole>(setupAction ?? DefaultSetupAction)
        .AddTokenProvider<PasswordTokenProvider<ApplicationUser>>("ResetPassword")
        .AddDefaultTokenProviders();

    private static void DefaultSetupAction(IdentityOptions options)
    {
        options.Password = new PasswordOptions
        {
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true,
            RequiredLength = 8,
            RequiredUniqueChars = 3,
        };

        options.Lockout = new LockoutOptions { AllowedForNewUsers = true, DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1), MaxFailedAccessAttempts = 3, };

        options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedEmail = true;
    }
}