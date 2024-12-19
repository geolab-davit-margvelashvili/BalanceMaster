using BalanceMaster.Domain.Constants;
using BalanceMaster.Domain.Models.Abstractions;
using BalanceMaster.Domain.Models.Enums;

namespace BalanceMaster.Domain.Models;

public class Operation : DomainEntity<Guid>
{
    public int AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public DateTime CreateAt { get; private set; }
    public OperationType OperationType { get; set; }

    private Operation()
    {
        Currency = CurrencyConstants.NationalCurrency;
    }

    public static Operation CreateDebitOperation(int accountId, decimal amount, string currency) => new Operation
    {
        AccountId = accountId,
        Amount = amount,
        Currency = currency,
        CreateAt = DateTime.Now,
        OperationType = OperationType.Debit
    };

    public static Operation CreateCreditOperation(int accountId, decimal amount, string currency) => new Operation
    {
        AccountId = accountId,
        Amount = amount,
        Currency = currency,
        CreateAt = DateTime.Now,
        OperationType = OperationType.Credit
    };
}