using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Commands;

public sealed class OpenAccountCommand : ICommand
{
    public required int CustomerId { get; set; }
    public required string Iban { get; set; }
    public required string Currency { get; set; }

    public void Validate()
    {
        if (CustomerId <= 0)
            throw new ValidationException("CustomerId must be positive");

        if (string.IsNullOrWhiteSpace(Iban))
            throw new ValidationException("Iban must not be empty");

        if (Iban.Length is <= 14 or >= 34)
            throw new ValidationException("Iban length must be between 14 and 34");

        if (string.IsNullOrWhiteSpace(Currency))
            throw new ValidationException("Currency must not be empty");

        if (Currency.Length != 3)
            throw new ValidationException("Currency length must be 3");
    }
}