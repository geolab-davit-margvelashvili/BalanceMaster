using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public class InMemoryUnitOfWork : IUnitOfWork
{
    public Task CommitAsync()
    {
        return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}