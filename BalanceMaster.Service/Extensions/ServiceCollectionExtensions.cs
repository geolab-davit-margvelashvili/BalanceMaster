using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BalanceMaster.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var serviceTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsClass && (x.Name.EndsWith("Service") || x.Name.EndsWith("Repository")))
            .Select(x => new
            {
                Implementation = x,
                Abstractions = x.GetInterfaces()
                    .Where(i => i.Name.EndsWith("Service") || i.Name.EndsWith("Repository"))
                    .ToList(),
            })
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            foreach (var abstraction in serviceType.Abstractions)
            {
                serviceCollection.AddScoped(abstraction, serviceType.Implementation);
            }
        }

        return serviceCollection;
    }
}