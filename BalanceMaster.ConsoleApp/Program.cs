using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Service.Extensions;
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