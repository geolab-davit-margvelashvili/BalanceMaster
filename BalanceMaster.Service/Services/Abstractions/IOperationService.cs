using BalanceMaster.Service.Commands;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IOperationService
{
    Task ExecuteAsync(DebitCommand command);

    Task ExecuteAsync(CreditCommand command);
}