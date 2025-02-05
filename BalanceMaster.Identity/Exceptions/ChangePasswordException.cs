using Microsoft.AspNetCore.Identity;

namespace BalanceMaster.Identity.Exceptions;

public class ChangePasswordException : IdentityException
{
    public ChangePasswordException()
    {
    }

    public ChangePasswordException(string message) : base(message)
    {
    }

    public ChangePasswordException(IEnumerable<IdentityError> errors, string message) : base(errors, message)
    {
    }
}