using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Models;
using BalanceMaster.FileRepository.Abstractions;

namespace BalanceMaster.FileRepository.Implementations;

public sealed class FileCustomerRepository : FileRepositoryBase<Customer, int>, ICustomerRepository
{
    private readonly ISequenceProvider _sequenceProvider;

    public FileCustomerRepository(ISequenceProvider sequenceProvider) : base("customers.json")
    {
        _sequenceProvider = sequenceProvider;
    }

    protected override Task<int> GenerateIdAsync() =>
        _sequenceProvider.GetNextInteger("customers");
}