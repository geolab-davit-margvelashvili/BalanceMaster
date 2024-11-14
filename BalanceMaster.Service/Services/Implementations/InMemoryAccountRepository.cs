using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts;

    public InMemoryAccountRepository()
    {
        _accounts = new List<Account>
        {
            new Account
            {
                Id = 1,
                Currency = "GEL",
                Balance = 100.50m,
                Iban = "GE17292367742213184252",
                CustomerId = 1,
                Overdraft = new Overdraft()
                {
                    Amount = 50m,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1)
                }
            },

            new Account
            {
                Id = 2,
                Currency = "USD",
                Balance = 1030.50m,
                Iban = "GE17292365721213154299",
                CustomerId = 2,
                Overdraft = new Overdraft()
                {
                    Amount = 50m,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1)
                }
            },
        };
    }

    public Account GetById(int id)
    {
        return _accounts.FirstOrDefault(account => account.Id == id);
    }

    public void SaveAccount(Account account)
    {
        // We have in memory collection of account so we do not write account anywhere
    }
}