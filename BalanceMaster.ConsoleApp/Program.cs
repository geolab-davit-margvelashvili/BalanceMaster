using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Extensions;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BalanceMaster.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
             .AutoRegisterApplicationServices()
             .BuildServiceProvider();

        var operationService = serviceProvider.GetRequiredService<IOperationService>();

        var json = File.ReadAllText("Data.json");
        var debitCommands = JsonSerializer.Deserialize<List<DebitCommand>>(json);

        foreach (var debitCommand in debitCommands)
        {
            operationService.ExecuteAsync(debitCommand);
        }
    }
}