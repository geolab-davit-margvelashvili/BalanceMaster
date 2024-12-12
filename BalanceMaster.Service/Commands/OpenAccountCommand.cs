namespace BalanceMaster.Service.Commands;

public class OpenAccountCommand
{
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
    public string Iban { get; set; }
    public string Currency { get; set; }
}