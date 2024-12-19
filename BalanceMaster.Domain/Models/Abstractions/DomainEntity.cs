namespace BalanceMaster.Domain.Models.Abstractions;

public abstract class DomainEntity<TId> where TId : IComparable<TId>
{
    public TId Id { get; set; } = default!;
}