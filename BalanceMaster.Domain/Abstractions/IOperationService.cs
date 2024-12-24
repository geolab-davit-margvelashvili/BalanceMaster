using BalanceMaster.Domain.Commands;

namespace BalanceMaster.Domain.Abstractions;

public interface IOperationService
{
    Task<Guid> ExecuteAsync(DebitCommand command);

    Task<Guid> ExecuteAsync(CreditCommand command);
}