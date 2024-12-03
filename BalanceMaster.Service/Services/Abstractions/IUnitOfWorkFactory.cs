namespace BalanceMaster.Service.Services.Abstractions;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateUnitOfWorkAsync();
}