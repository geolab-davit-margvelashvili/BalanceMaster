using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Account>> ListAsync(AccountQueryFilter? filter)
    {
        var result = await _databaseContext
            .Accounts
            .AsNoTracking()
            .Where(x => x.Balance >= filter.MinBalance && x.Balance <= filter.MaxBalance)
            .ToListAsync();

        return result;
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

    public async Task UpdateAsync(Account account)
    {
        _databaseContext.Accounts.Attach(account);
        await _databaseContext.SaveChangesAsync();
    }
}