using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IAccountRepository
{
    Account GetById(int id);

    void SaveAccount(Account account);
}