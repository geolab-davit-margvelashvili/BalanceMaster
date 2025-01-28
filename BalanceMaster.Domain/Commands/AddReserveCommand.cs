using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Exceptions;

namespace BalanceMaster.Domain.Commands;

public sealed class AddReserveCommand : ICommand
{
    public required int AccountId { get; set; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; }
    public required string Currency { get; set; }

    public void Validate()
    {
        if (AccountId <= 0)
            throw new ValidationException("Id must be positive");

        if (Amount <= 0)
            throw new ValidationException("Amount must be positive");

        if (string.IsNullOrWhiteSpace(Currency))
            throw new ValidationException("Currency must not be empty");

        if (Currency.Length != 3)
            throw new ValidationException("Currency length must be 3");

        if (Description is not null)
        {
            if (Description.Length > 200)
            {
                throw new ValidationException("Description length must be less than or equal to 200");
            }
        }
    }
}