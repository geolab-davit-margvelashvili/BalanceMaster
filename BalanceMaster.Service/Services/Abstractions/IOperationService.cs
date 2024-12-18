using BalanceMaster.Service.Commands;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IOperationService
{
    Task<Guid> ExecuteAsync(DebitCommand command);

    Task<Guid> ExecuteAsync(CreditCommand command);
}