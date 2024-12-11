using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(int id);

    Task<List<Account>> ListAsync();

    Task<Account?> GetByIdOrDefaultAsync(int id);

    Task SaveAccountAsync(Account account);
}