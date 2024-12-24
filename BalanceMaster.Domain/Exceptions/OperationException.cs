namespace BalanceMaster.Domain.Exceptions;

public class OperationException : DomainException
{
    public OperationException()
    {
    }

    public OperationException(string operationName, string reason) : base($"{operationName} is restricted because {reason}")
    {
    }
}