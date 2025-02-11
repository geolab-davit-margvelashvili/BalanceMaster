namespace BalanceMaster.Identity.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base("Invalid username or password")
    {
    }

    public AuthenticationException(string message) : base(message)
    {
    }
}