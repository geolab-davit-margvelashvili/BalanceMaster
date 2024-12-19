namespace BalanceMaster.Domain.Models;

public class Overdraft
{
    public decimal Amount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool IsActive => StartDate is null || DateTime.Now >= StartDate && (EndDate is null || DateTime.Now <= EndDate);

    public decimal GetAmount() => IsActive ? Amount : 0;
}