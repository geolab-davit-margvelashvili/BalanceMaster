using BalanceMaster.Domain.Models;

namespace BalanceMaster.Domain.Abstractions;

public interface IOperationRepository
{
    Task<Guid> CreateAsync(Operation operation);
}