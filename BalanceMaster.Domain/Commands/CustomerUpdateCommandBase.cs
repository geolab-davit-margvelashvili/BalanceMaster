using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Exceptions;

namespace BalanceMaster.Domain.Commands;

public abstract class CustomerUpdateCommandBase : ICommand
{
    public int CustomerId { get; set; }

    public virtual void Validate()
    {
        if (CustomerId <= 0)
            throw new ValidationException("CustomerId must be positive");
    }
}