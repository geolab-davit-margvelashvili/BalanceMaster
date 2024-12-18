using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Commands;

public sealed class CloseAccountCommand : ICommand
{
    public required int Id { get; set; }

    public void Validate()
    {
        if (Id <= 0)
            throw new ValidationException("Id must be positive");
    }
}