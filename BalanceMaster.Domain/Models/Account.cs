﻿using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models.Abstractions;
using BalanceMaster.Domain.Models.Enums;

namespace BalanceMaster.Domain.Models;

public class Account : DomainEntity<int>
{
    public int CustomerId { get; }
    public string Iban { get; }
    public string Currency { get; }
    public decimal Balance { get; private set; }

    public AccountStatus Status { get; set; }

    public Overdraft? Overdraft { get; private set; }

    public Account(int id, int customerId, string iban, string currency, decimal balance, Overdraft? overdraft)
    {
        Id = id;
        Iban = iban;
        Currency = currency;
        Balance = balance;
        CustomerId = customerId;
        Overdraft = overdraft;
        Status = AccountStatus.Open;
    }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Account cannot debit non positive amount");

        if (Status != AccountStatus.Open)
            throw new OperationException("Debit", $"account status is {Status}");

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Account cannot credit non positive amount");

        if (Status != AccountStatus.Open)
            throw new OperationException("Credit", $"account status is {Status}");

        Balance += amount;
    }

    public void CloseOverdraft()
    {
        Overdraft = null;
    }

    public void OpenOverdraft(Overdraft overdraft)
    {
        // TODO: Add validation if overdraft is already open
        Overdraft = overdraft;
    }

    public decimal GetBalance()
    {
        if (Overdraft is null)
            return Balance;

        return Balance + Overdraft.GetAmount();
    }

    public void Close()
    {
        if (Balance != 0)
            throw new OperationException("Closing account", "account balance is not 0");

        CloseOverdraft();
        Status = AccountStatus.Closed;
    }
}