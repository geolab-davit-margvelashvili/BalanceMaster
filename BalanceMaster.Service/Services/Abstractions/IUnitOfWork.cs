namespace BalanceMaster.Service.Services.Abstractions;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    void Start();

    Task CompleteAsync();
}