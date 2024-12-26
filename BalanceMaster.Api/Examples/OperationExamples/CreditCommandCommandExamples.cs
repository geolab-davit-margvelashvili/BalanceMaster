using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.OperationExamples;

public sealed class CreditCommandCommandExamples : IMultipleExamplesProvider<CreditCommand>
{
    public IEnumerable<SwaggerExample<CreditCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Credit Example", new CreditCommand
        {
            AccountId = 4,
            Amount = 10,
            Currency = "GEL",
        });
    }
}