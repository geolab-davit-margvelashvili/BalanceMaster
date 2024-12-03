namespace BalanceMaster.Service.Services.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();

    Task RollbackAsync();
}