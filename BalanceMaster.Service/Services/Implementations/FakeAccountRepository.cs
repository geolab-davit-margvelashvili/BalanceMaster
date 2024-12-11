using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

[AttributeUsage(validOn: AttributeTargets.Class)]
public class ExcludeFromServiceCollectionAttribute : Attribute
{
}

[ExcludeFromServiceCollection]
public class FakeAccountRepository : IAccountRepository
{
    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Account?> GetByIdOrDefaultAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }
}