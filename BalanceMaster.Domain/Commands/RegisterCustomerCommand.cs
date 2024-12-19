using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Exceptions;

namespace BalanceMaster.Domain.Commands;

public sealed class RegisterCustomerCommand : ICommand
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PrivateNumber { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ValidationException("First name must not be empty");

        if (string.IsNullOrWhiteSpace(LastName))
            throw new ValidationException("Last name must not be empty");

        if (string.IsNullOrWhiteSpace(PrivateNumber))
            throw new ValidationException("Private number must not be empty");

        if (PrivateNumber.Length != 11 || !PrivateNumber.All(char.IsDigit))
            throw new ValidationException("Private number must be 11 digits long and contain only numbers");
    }
}