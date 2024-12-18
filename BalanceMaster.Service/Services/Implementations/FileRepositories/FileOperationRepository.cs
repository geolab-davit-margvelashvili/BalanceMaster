using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations.FileRepositories;

public sealed class FileOperationRepository : FileRepositoryBase<Operation, Guid>, IOperationRepository
{
    public FileOperationRepository() : base("operations.json")
    {
    }

    protected override Task<Guid> GenerateIdAsync() => Task.FromResult(Guid.NewGuid());
}