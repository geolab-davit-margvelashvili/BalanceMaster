using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Mappings;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations.Services;

public sealed class OperationService : IOperationService
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

        await _operationRepository.CreateAsync(operation);
        await _accountRepository.CreateAsync(account);
    }

    public async Task ExecuteAsync(CreditCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        account.Credit(command.Amount);

        var operation = command.ToOperation();

        await _operationRepository.CreateAsync(operation);
        await _accountRepository.CreateAsync(account);
    }
}