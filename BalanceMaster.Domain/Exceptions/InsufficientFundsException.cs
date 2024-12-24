namespace BalanceMaster.Domain.Exceptions;

public class InsufficientFundsException : DomainException
{
    public InsufficientFundsException(int accountId)
        : base($"Insufficient funds on account with id: {accountId}")
    {
    }
}