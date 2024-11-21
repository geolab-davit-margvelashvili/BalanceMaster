namespace BalanceMaster.Service.Models;

public class Account
{
    public int Id { get; set; }
    public required string Iban { get; set; }
    public required string Currency { get; set; }
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }

    public Overdraft? Overdraft { get; set; }

    public decimal GetBalance()
    {
        if (Overdraft is null)
            return Balance;

        return Balance + Overdraft.GetAmount();
    }
}