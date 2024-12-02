using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public sealed class InMemoryOperationRepository : IOperationRepository
{
    private static readonly Dictionary<Guid, Operation> Operations = new();

    public Task SaveOperation(Operation operation)
    {
        Operations[operation.Id] = operation;
        return Task.CompletedTask;
    }
}