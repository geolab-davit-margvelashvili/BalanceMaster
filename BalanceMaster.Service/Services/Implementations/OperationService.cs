using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public class OperationService
{
    private readonly IAccountRepository _accountRepository;

    public OperationService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Execute(DebitCommand command)
    {
        command.Validate();

        var account = _accountRepository.GetById(command.AccountId);

        if (account.GetBalance() < command.Amount)
        {
            throw new InsufficientFundsException(account.Id);
        }

        account.Balance -= command.Amount;

        _accountRepository.SaveAccount(account);
    }

    public void Execute(CreditCommand command)
    {
        command.Validate();

        var account = _accountRepository.GetById(command.AccountId);

        account.Balance += command.Amount;

        _accountRepository.SaveAccount(account);
    }
}