using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Services.Implementations;
using System.Text.Json;

namespace BalanceMaster.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var accountRepository = new InMemoryAccountRepository();
        var operationService = new OperationService(accountRepository);

        var json = File.ReadAllText("Data.json");

        var debitCommands = JsonSerializer.Deserialize<List<DebitCommand>>(json);
        foreach (var debitCommand in debitCommands)
        {
            operationService.Execute(debitCommand);
        }
    }
}