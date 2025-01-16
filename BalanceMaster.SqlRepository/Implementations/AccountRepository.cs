using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _databaseContext;

    public AccountRepository(AppDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        return await GetByIdOrDefaultAsync(id)
               ?? throw new ObjectNotFoundException(id.ToString(), nameof(Account));
    }

    public Task<List<Account>> ListAsync(AccountQueryFilter? filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Account?> GetByIdOrDefaultAsync(int id)
    {
        return await _databaseContext.Accounts.FindAsync(id);
    }

    public async Task<int> CreateAsync(Account account)
    {
        await _databaseContext.Accounts.AddAsync(account);
        await _databaseContext.SaveChangesAsync();
        return account.Id;
    }

    public Task UpdateAsync(Account account)
    {
        throw new NotImplementedException();
    }
}