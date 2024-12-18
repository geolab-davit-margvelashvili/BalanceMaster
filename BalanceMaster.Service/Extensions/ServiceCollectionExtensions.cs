using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.Service.Services.Implementations.FileRepositories;
using BalanceMaster.Service.Services.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BalanceMaster.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AutoRegisterApplicationServices(this IServiceCollection serviceCollection)
    {
        var typesToRegister = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")))
            .Select(type => (Implementation: type, Interfaces: type.GetInterfaces()))
            .ToList();

        foreach (var type in typesToRegister)
        {
            foreach (var typeInterface in type.Interfaces)
            {
                serviceCollection.AddScoped(typeInterface, type.Implementation);
            }
        }

        return serviceCollection;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection) => serviceCollection
            .AddRepositories()
            .AddServices();

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection) => serviceCollection
        .AddScoped<ISequenceProvider, FileSequenceProvider>()
        .AddScoped<IOperationRepository, FileOperationRepository>()
        .AddScoped<IAccountRepository, FileAccountRepository>()
        .AddScoped<ICustomerRepository, FileCustomerRepository>();

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection) => serviceCollection
        .AddScoped<IOperationService, OperationService>()
        .AddScoped<ICustomerService, CustomerService>()
        .AddScoped<IAccountService, AccountService>();
}