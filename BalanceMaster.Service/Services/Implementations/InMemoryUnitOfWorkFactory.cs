using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public sealed class InMemoryUnitOfWorkFactory : IUnitOfWorkFactory
{
    public Task<IUnitOfWork> CreateUnitOfWorkAsync()
    {
        return Task.FromResult<IUnitOfWork>(new InMemoryUnitOfWork());
    }
}