using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BalanceMaster.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = RegisterServices().BuildServiceProvider();
        var operationService = serviceProvider.GetRequiredService<IOperationService>();

        var json = File.ReadAllText("Data.json");
        var debitCommands = JsonSerializer.Deserialize<List<DebitCommand>>(json);

        foreach (var debitCommand in debitCommands)
        {
            operationService.ExecuteAsync(debitCommand);
        }
    }

    public static IServiceCollection RegisterServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IOperationService, OperationService>();
        serviceCollection.AddScoped<IAccountRepository, InMemoryAccountRepository>();

        return serviceCollection;
    }
}