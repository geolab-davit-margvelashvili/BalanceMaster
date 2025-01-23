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
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public OperationService(IAccountRepository accountRepository, IOperationRepository operationRepository, IUnitOfWorkFactory unitOfWorkFactory)
    {
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
        _unitOfWorkFactory = unitOfWorkFactory;
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
        var operationId = await _operationRepository.CreateAsync(operation);

        return operationId;
    }

    public async Task<Guid> ExecuteAsync(CreditCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId);

        account.Credit(command.Amount);

        var operation = command.ToOperation();

        await using var unitOfWork = await _unitOfWorkFactory.StartUnitOfWorkAsync();

        await _accountRepository.UpdateAsync(account);
        var operationId = await _operationRepository.CreateAsync(operation);

        await unitOfWork.CommitAsync();

        return operationId;
    }
}