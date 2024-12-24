using BalanceMaster.Domain.Models;
using BalanceMaster.FileRepository.Abstractions;
using BalanceMaster.FileRepository.Models;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace BalanceMaster.FileRepository.Implementations;

public sealed class FileOperationRepository : FileRepositoryBase<Operation, Guid>, IOperationRepository
{
    public FileOperationRepository(IOptions<FileStorageOptions> options)
        : base(options.Value.OperationRepositoryPath)
    {
    }

    protected override Task<Guid> GenerateIdAsync() => Task.FromResult(Guid.NewGuid());
}