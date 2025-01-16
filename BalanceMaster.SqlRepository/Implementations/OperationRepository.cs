using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.SqlRepository.Implementations;

internal class OperationRepository : IOperationRepository
{
    public Task<Guid> CreateAsync(Operation operation)
    {
        throw new NotImplementedException();
    }
}