using BalanceMaster.MessageSender.Abstractions.Models;
using BalanceMaster.MessageSender.Abstractions.Services;
using BalanceMaster.MessageSender.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BalanceMaster.MessageSender.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderOptions.Key));

        return services;
    }
}