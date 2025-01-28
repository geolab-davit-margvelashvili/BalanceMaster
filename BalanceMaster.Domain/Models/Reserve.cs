namespace BalanceMaster.Domain.Models;

public class Reserve
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; }
    public required string Currency { get; set; }
}