using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(int id);

    Task<List<Account>> ListAsync(AccountQueryFilter? filter);

    Task<Account?> GetByIdOrDefaultAsync(int id);

    Task<int> CreateAsync(Account account);

    Task UpdateAsync(Account account);
}