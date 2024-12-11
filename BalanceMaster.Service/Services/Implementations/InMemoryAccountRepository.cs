using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts;

    public InMemoryAccountRepository()
    {
        _accounts = new List<Account>
        {
            new Account
            (
                id : 1,
                currency : "GEL",
                balance : 100.50m,
                iban : "GE17292367742213184252",
                customerId : 1,
                overdraft : new Overdraft()
                {
                    Amount = 50m,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1)
                }
            ),

            new Account
            (
                id : 2,
                currency : "USD",
                balance : 1030.50m,
                iban : "GE17292365721213154299",
                customerId : 2,
                overdraft : new Overdraft()
                {
                    Amount = 50m,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1)
                }
            ),
        };
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        var account = await GetByIdOrDefaultAsync(id);
        if (account is null)
        {
            throw new ObjectNotFoundException(id.ToString(), nameof(Account));
        }

        return account;
    }

    public Task<List<Account>> ListAsync()
    {
        return Task.FromResult(_accounts);
    }

    public Task<Account?> GetByIdOrDefaultAsync(int id)
    {
        var account = _accounts.FirstOrDefault(account => account.Id == id); ;
        return Task.FromResult(account);
    }

    public Task SaveAccountAsync(Account account)
    {
        return Task.CompletedTask;
        // We have in memory collection of account so we do not write account anywhere
    }
}