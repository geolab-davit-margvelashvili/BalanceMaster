namespace BalanceMaster.Service.Models;

public class Overdraft
{
    public decimal Amount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public decimal GetAmount()
    {
        var now = DateTime.Now;
        if (StartDate is null && EndDate is null)
            return Amount;

        if (StartDate.HasValue && EndDate.HasValue)
        {
            return now >= StartDate && now <= EndDate ? Amount : 0;
        }

        if (StartDate.HasValue && EndDate is null)
        {
            return now >= StartDate ? Amount : 0;
        }

        if (StartDate is null && EndDate.HasValue)
        {
            return now <= EndDate ? Amount : 0;
        }

        return Amount;
    }
}