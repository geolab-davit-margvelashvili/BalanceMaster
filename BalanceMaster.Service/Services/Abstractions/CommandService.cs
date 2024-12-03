using BalanceMaster.Service.Commands;
using System.Reflection;

namespace BalanceMaster.Service.Services.Abstractions;

public abstract class CommandService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public CommandService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task ExecuteAsync<TCommand>(TCommand command)
    {
        var unitOfWork = await _unitOfWorkFactory.CreateUnitOfWorkAsync();

        var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.FlattenHierarchy);

        var method = methods.FirstOrDefault(x => x.Name == "Handle" && x.GetParameters().Any(x => x.ParameterType == typeof(TCommand)));
        var result = method?.Invoke(this, new object[] { command });
        if (result is Task taskResult)
        {
            await taskResult;
        }

        await unitOfWork.CommitAsync();
    }
}

public class TestService : CommandService
{
    public TestService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
    {
    }

    private Task Handle(DebitCommand command)
    {
        return Task.CompletedTask;
    }
}