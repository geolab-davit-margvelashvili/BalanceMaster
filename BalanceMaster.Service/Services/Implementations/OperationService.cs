using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Mappings;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations;

public class OperationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;

    public OperationService(IAccountRepository accountRepository, IOperationRepository operationRepository)
    {
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
    }

    public async Task ExecuteAsync(DebitCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        if (account.GetBalance() < command.Amount)
        {
            throw new InsufficientFundsException(account.Id);
        }

        account.Debit(command.Amount);

        var operation = command.ToOperation();

        await _operationRepository.SaveOperation(operation);
        await _accountRepository.SaveAccountAsync(account);
    }

    public async Task ExecuteAsync(CreditCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        account.Credit(command.Amount);

        var operation = command.ToOperation();

        await _operationRepository.SaveOperation(operation);
        await _accountRepository.SaveAccountAsync(account);
    }
}