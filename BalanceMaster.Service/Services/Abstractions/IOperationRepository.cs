using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IOperationRepository
{
    Task SaveOperation(Operation operation);
}