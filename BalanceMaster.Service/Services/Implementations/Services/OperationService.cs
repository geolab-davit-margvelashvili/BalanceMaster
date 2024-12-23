using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Exceptions;
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

    public async Task<Guid> ExecuteAsync(DebitCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        if (account.GetBalance() < command.Amount)
        {
            throw new InsufficientFundsException(account.Id);
        }

        account.Debit(command.Amount);

        var operation = command.ToOperation();

        await _accountRepository.UpdateAsync(account);
        return await _operationRepository.CreateAsync(operation);
    }

    public async Task<Guid> ExecuteAsync(CreditCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        account.Credit(command.Amount);

        var operation = command.ToOperation();

        await _accountRepository.UpdateAsync(account);
        return await _operationRepository.CreateAsync(operation);
    }
}