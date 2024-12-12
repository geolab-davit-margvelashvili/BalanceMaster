using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(int id);

    Task<List<Account>> ListAsync(QueryFilter? filter);

    Task<Account?> GetByIdOrDefaultAsync(int id);

    Task SaveAccountAsync(Account account);
}

public class QueryFilter
{
    public decimal? MinBalance { get; set; }
    public decimal? MaxBalance { get; set; }
    public bool? IsActive { get; set; }
    public string? Currency { get; set; }
    public bool? IsDebitAccount { get; set; }
}