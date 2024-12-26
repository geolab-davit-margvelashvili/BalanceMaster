using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.OperationExamples;

public sealed class DebitCommandCommandExamples : IMultipleExamplesProvider<DebitCommand>
{
    public IEnumerable<SwaggerExample<DebitCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Credit Example", new DebitCommand
        {
            AccountId = 4,
            Amount = 10,
            Currency = "GEL",
        });
    }
}