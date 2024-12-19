using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations.FileRepositories;

public sealed class FileAccountRepository : FileRepositoryBase<Account, int>, IAccountRepository
{
    private readonly ISequenceProvider _sequenceProvider;

    public FileAccountRepository(ISequenceProvider sequenceProvider) : base("accounts.json")
    {
        _sequenceProvider = sequenceProvider;
    }

    public async Task<List<Account>> ListAsync(AccountQueryFilter? filter)
    {
        if (filter is null)
            return await ListAsync();

        IEnumerable<Account> accounts = await ListAsync();

        if (filter.Currency is not null)
            accounts = accounts.Where(x => x.Currency == filter.Currency);

        if (filter.Iban is not null)
            accounts = accounts.Where(x => x.Iban == filter.Iban);

        if (filter.MinBalance is not null)
            accounts = accounts.Where(x => x.Balance >= filter.MinBalance.Value);

        if (filter.MaxBalance is not null)
            accounts = accounts.Where(x => x.Balance <= filter.MaxBalance.Value);

        return accounts.ToList();
    }

    protected override Task<int> GenerateIdAsync() =>
        _sequenceProvider.GetNextInteger("accounts");
}