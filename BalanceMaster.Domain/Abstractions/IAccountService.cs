using BalanceMaster.Domain.Commands;

namespace BalanceMaster.Domain.Abstractions;

public interface IAccountService
{
    Task ExecuteAsync(OpenAccountCommand command);

    Task ExecuteAsync(CloseAccountCommand command);
}