using Microsoft.AspNetCore.Identity;

namespace BalanceMaster.Identity.Exceptions;

public class IdentityException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; }

    public IdentityException()
    {
    }

    public IdentityException(string message) : base(message)
    {
    }

    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public IdentityException(IEnumerable<IdentityError> errors, string message) : base(message)
    {
        Errors = errors;
    }
}