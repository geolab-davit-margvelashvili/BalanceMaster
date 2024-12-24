using BalanceMaster.Domain.Commands;

namespace BalanceMaster.Domain.Abstractions;

public interface IAccountService
{
    Task<int> ExecuteAsync(OpenAccountCommand command);

    Task ExecuteAsync(CloseAccountCommand command);
}