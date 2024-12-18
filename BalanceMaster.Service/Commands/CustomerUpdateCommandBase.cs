using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Commands;

public abstract class CustomerUpdateCommandBase : ICommand
{
    public int CustomerId { get; set; }

    public virtual void Validate()
    {
        if (CustomerId <= 0)
            throw new ValidationException("CustomerId must be positive");
    }
}