using BalanceMaster.Service.Constants;
using BalanceMaster.Service.Models.Enums;

namespace BalanceMaster.Service.Models;

public class Operation
{
    public Guid Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = CurrencyConstants.NationalCurrency;
    public DateTime CreateAt { get; set; }
    public OperationType OperationType { get; set; }

    private Operation()
    {
    }

    public static Operation CreateDebitOperation(int accountId, decimal amount, string currency) => new Operation
    {
        Id = Guid.NewGuid(),
        AccountId = accountId,
        Amount = amount,
        Currency = currency,
        CreateAt = DateTime.Now,
        OperationType = OperationType.Debit
    };

    public static Operation CreateCreditOperation(int accountId, decimal amount, string currency) => new Operation
    {
        Id = Guid.NewGuid(),
        AccountId = accountId,
        Amount = amount,
        Currency = currency,
        CreateAt = DateTime.Now,
        OperationType = OperationType.Credit
    };
}