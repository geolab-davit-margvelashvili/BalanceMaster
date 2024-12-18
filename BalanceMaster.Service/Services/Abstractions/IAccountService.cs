using BalanceMaster.Service.Commands;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IAccountService
{
    Task ExecuteAsync(OpenAccountCommand command);

    Task ExecuteAsync(CloseAccountCommand command);
}