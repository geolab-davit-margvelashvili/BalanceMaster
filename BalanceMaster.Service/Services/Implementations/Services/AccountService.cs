using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;

namespace BalanceMaster.Service.Services.Implementations.Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task ExecuteAsync(OpenAccountCommand command)
    {
        var account = new Account(1, command.CustomerId, command.Iban, command.Currency, 0m, null);

        await _accountRepository.CreateAsync(account);
    }

    public async Task ExecuteAsync(CloseAccountCommand command)
    {
        var account = await _accountRepository.GetByIdAsync(command.Id);
        account.Close();

        await _accountRepository.CreateAsync(account);
    }
}