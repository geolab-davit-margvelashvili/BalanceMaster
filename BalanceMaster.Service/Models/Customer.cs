using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Models.Enums;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Models;

public sealed class Customer : DomainEntity<int>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PrivateNumber { get; private set; }
    public CustomerStatus Status { get; private set; }

    private Customer()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PrivateNumber = string.Empty;
        Status = CustomerStatus.Active;
    }

    public Customer(string firstName, string lastName, string privateNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PrivateNumber = privateNumber;
        Status = CustomerStatus.Active;
    }

    public void Close()
    {
        Status = CustomerStatus.Closed;
    }

    public void Open()
    {
        Status = CustomerStatus.Active;
    }

    public void Suspend()
    {
        if (Status is CustomerStatus.Closed)
            throw new OperationException("suspend customer", "customer is closed");

        Status = CustomerStatus.Suspended;
    }
}