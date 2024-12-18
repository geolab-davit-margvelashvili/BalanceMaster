using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.Service.Services.Implementations.Services;
using FluentAssertions;
using Moq;

namespace BalanceMaster.Service.Tests;

public class OperationServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepository;
    private readonly Mock<IOperationRepository> _operationRepository;

    public OperationServiceTests()
    {
        _accountRepository = new Mock<IAccountRepository>();
        _operationRepository = new Mock<IOperationRepository>();
    }

    [Fact(DisplayName = "დებეტის ოპერაცია ანგარიშზე ბალანსს უნდა ამცირებდეს")]
    public void Debit_Should_ReduceBalanceOnAccount()
    {
        // Arrange
        var account = new Account
        (
            id: 1,
            customerId: 1,
            iban: "Account1",
            currency: "GEL",
            balance: 100,
            overdraft: null
        );

        _accountRepository
            .Setup(x => x.GetByIdAsync(account.Id))
            .ReturnsAsync(account);

        var service = new OperationService(_accountRepository.Object, _operationRepository.Object);
        var debitCommand = new DebitCommand
        {
            Amount = 20,
            Currency = account.Currency,
            AccountId = account.Id
        };

        // Act
        service.ExecuteAsync(debitCommand);

        // Assert
        Assert.Equal(80, account.Balance);
    }

    [Fact(DisplayName = "დებეტის ოპერაცია ანგარიშზე ბალანსს უნდა ამცირებდეს თუ ბალანსი 0-ია მაგრამ აქვს ოვედრაფტი")]
    public void Debit_Should_ReduceBalanceOnAccount_If_HasOverdraft_And_ZeroBalance()
    {
        // Arrange
        var account = new Account
        (
            id: 1,
            customerId: 1,
            iban: "Account1",
            currency: "GEL",
            balance: 0,
            overdraft: new Overdraft
            {
                Amount = 100,
                StartDate = DateTime.Today.AddDays(-1),
                EndDate = DateTime.Today.AddDays(1)
            }
        );
        _accountRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(account);

        var service = new OperationService(_accountRepository.Object, _operationRepository.Object);
        var debitCommand = new DebitCommand
        {
            Amount = 20,
            Currency = "GEL",
            AccountId = 1
        };

        // Act
        service.ExecuteAsync(debitCommand);

        // Assert
        account.Balance.Should().Be(-20);
    }
}