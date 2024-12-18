using BalanceMaster.Service.Services.Abstractions;
using ValidationException = BalanceMaster.Service.Exceptions.ValidationException;

namespace BalanceMaster.Service.Commands;

public abstract class OperationCommandBase : ICommand
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }

    public void Validate()
    {
        if (AccountId <= 0)
            throw new ValidationException("AccountId must be positive");

        if (Amount < 0)
            throw new ValidationException("Amount must not be negative");

        if (string.IsNullOrWhiteSpace(Currency))
            throw new ValidationException("Currency must not be empty");

        if (Currency.Length != 3)
            throw new ValidationException("Currency length must be 3");
    }
}