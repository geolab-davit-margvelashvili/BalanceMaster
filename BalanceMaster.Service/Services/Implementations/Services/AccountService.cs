﻿using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations.Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<int> ExecuteAsync(OpenAccountCommand command)
    {
        var account = new Account(0, command.CustomerId, command.Iban, command.Currency, 0m, null, null);
        return await _accountRepository.CreateAsync(account);
    }

    public async Task ExecuteAsync(CloseAccountCommand command)
    {
        var account = await _accountRepository.GetByIdAsync(command.Id, false);
        account.Close();

        await _accountRepository.CreateAsync(account);
    }

    public async Task ExecuteAsync(AddReserveCommand command)
    {
        command.Validate();

        var account = await _accountRepository.GetByIdAsync(command.AccountId, true);
        var reserve = new Reserve
        {
            AccountId = command.AccountId,
            Currency = command.Currency,
            Amount = command.Amount,
            Description = command.Description
        };
        account.AddReserve(reserve);

        await _accountRepository.UpdateAsync(account);
    }
}