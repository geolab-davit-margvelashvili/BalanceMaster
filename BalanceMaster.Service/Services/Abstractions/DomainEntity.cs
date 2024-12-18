namespace BalanceMaster.Service.Services.Abstractions;

public abstract class DomainEntity<TId> where TId : IComparable<TId>
{
    public TId Id { get; set; } = default!;
}