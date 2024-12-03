using BalanceMaster.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BalanceMaster.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var typesToRegister = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")))
            .Where(t => t.GetCustomAttribute<ExcludeFromServiceCollectionAttribute>() == null)
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

    private static (int id, int value) Function()
    {
        return (1, 2);
    }
}