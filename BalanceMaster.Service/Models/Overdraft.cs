namespace BalanceMaster.Service.Models;

public class Overdraft
{
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}