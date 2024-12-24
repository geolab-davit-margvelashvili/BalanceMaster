namespace BalanceMaster.Domain.Exceptions;

public class ObjectNotFoundException : DomainException
{
    public ObjectNotFoundException(string id, string objectType)
        : base($"{objectType} with id: '{id}' not found")
    {
    }
}