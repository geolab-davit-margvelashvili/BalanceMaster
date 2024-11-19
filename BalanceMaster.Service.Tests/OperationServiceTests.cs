using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.Service.Services.Implementations;
using FluentAssertions;
using Moq;

namespace BalanceMaster.Service.Tests;

public class MockAccountRepository : IAccountRepository
{
    private Account _account = new Account
    {
        Id = 1,
        Iban = "Account1",
        Balance = 100,
        Currency = "GEL",
        CustomerId = 1,
        Overdraft = new Overdraft
        {
            Amount = 20,
            StartDate = DateTime.Today.AddDays(-1),
            EndDate = DateTime.Today.AddDays(1),
        },
    };

    public Account GetById(int id)
    {
        return id == 1 ? _account : null;
    }

    public void SaveAccount(Account account)
    {
    }
}

public class OperationServiceTests
{
    [Fact(DisplayName = "დებეტის ოპერაცია ანგარიშზე ბალანსს უნდა ამცირებდეს")]
    public void Debit_Should_ReduceBalanceOnAccount()
    {
        // Arrange
        var repository = new MockAccountRepository();
        var service = new OperationService(repository);
        var debitCommand = new DebitCommand
        {
            Amount = 20,
            Currency = "GEL",
            AccountId = 1
        };
        var account = repository.GetById(1);

        // Act
        service.Execute(debitCommand);

        // Assert
        Assert.Equal(80, account.Balance);
    }

    [Fact(DisplayName = "დებეტის ოპერაცია ანგარიშზე ბალანსს უნდა ამცირებდეს თუ ბალანსი 0-ია მაგრამ აქვს ოვედრაფტი")]
    public void Debit_Should_ReduceBalanceOnAccount_If_HasOverdraft_And_ZeroBalance()
    {
        // Arrange
        //var repository = new MockAccountRepository();

        var account = new Account
        {
            Id = 1,
            Currency = "GEL",
            Balance = 0,
            Overdraft = new Overdraft
            {
                Amount = 100,
                StartDate = DateTime.Today.AddDays(-1),
                EndDate = DateTime.Today.AddDays(1)
            },
        };
        var repository = new Mock<IAccountRepository>();
        repository
            .Setup(x => x.GetById(1))
            .Returns(account);

        var service = new OperationService(repository.Object);
        var debitCommand = new DebitCommand
        {
            Amount = 20,
            Currency = "GEL",
            AccountId = 1
        };

        // Act
        service.Execute(debitCommand);

        // Assert
        account.Balance.Should().Be(-20);
    }
}