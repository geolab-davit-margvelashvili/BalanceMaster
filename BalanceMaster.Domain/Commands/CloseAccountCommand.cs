using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Exceptions;

namespace BalanceMaster.Domain.Commands;

public sealed class CloseAccountCommand : ICommand
{
    public required int Id { get; set; }

    public void Validate()
    {
        if (Id <= 0)
            throw new ValidationException("Id must be positive");
    }
}