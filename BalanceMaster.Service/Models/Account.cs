using BalanceMaster.Service.Exceptions;

namespace BalanceMaster.Service.Models;

public class Account
{
    public int Id { get; }
    public int CustomerId { get; }

    public string Iban { get; }
    public string Currency { get; }
    public decimal Balance { get; private set; }

    public Overdraft? Overdraft { get; private set; }

    public Account(int id, int customerId, string iban, string currency, decimal balance, Overdraft? overdraft)
    {
        Id = id;
        Iban = iban;
        Currency = currency;
        Balance = balance;
        CustomerId = customerId;
        Overdraft = overdraft;
    }

    public void Debit(decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Account cannot debit negative amount");

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Account cannot credit negative amount");

        Balance += amount;
    }

    public void CloseOverdraft(Overdraft overdraft)
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
}