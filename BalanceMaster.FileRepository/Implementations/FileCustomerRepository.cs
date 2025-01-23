using BalanceMaster.Domain.Models;
using BalanceMaster.FileRepository.Abstractions;
using BalanceMaster.FileRepository.Models;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace BalanceMaster.FileRepository.Implementations;

public sealed class FileCustomerRepository : FileRepositoryBase<Customer, int>, ICustomerRepository
{
    private readonly ISequenceProvider _sequenceProvider;

    public FileCustomerRepository(ISequenceProvider sequenceProvider, IOptions<FileStorageOptions> options)
        : base(options.Value.CustomerRepositoryPath)
    {
        _sequenceProvider = sequenceProvider;
    }

    protected override Task<int> GenerateIdAsync() =>
        _sequenceProvider.GetNextInteger("customers");

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}