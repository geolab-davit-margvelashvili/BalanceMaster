namespace BalanceMaster.Service.Services.Abstractions;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task RollBackAsync();

    Task CommitAsync();
}

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> StartUnitOfWorkAsync();
}