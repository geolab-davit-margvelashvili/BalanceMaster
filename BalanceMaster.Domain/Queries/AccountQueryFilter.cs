namespace BalanceMaster.Domain.Queries;

public class AccountQueryFilter
{
    public decimal? MinBalance { get; set; }
    public decimal? MaxBalance { get; set; }
    public string? Iban { get; set; }
    public string? Currency { get; set; }
}