namespace BalanceMaster.Service.Services.Abstractions;

public interface IUnitOfWork : IDisposable
{
    void Start();

    Task CompleteAsync();
}