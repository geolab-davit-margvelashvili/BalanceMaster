using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Models;
using BalanceMaster.FileRepository.Abstractions;

namespace BalanceMaster.FileRepository.Implementations;

public sealed class FileOperationRepository : FileRepositoryBase<Operation, Guid>, IOperationRepository
{
    public FileOperationRepository() : base("operations.json")
    {
    }

    protected override Task<Guid> GenerateIdAsync() => Task.FromResult(Guid.NewGuid());
}