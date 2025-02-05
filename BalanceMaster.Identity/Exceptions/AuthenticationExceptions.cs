namespace BalanceMaster.Identity.Exceptions;

public class AuthenticationExceptions : Exception
{
    public AuthenticationExceptions() : base("Invalid username or password")
    {
    }

    public AuthenticationExceptions(string message) : base(message)
    {
    }
}