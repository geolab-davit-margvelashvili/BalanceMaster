using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples;

public sealed class OpenAccountCommandExamples : IMultipleExamplesProvider<OpenAccountCommand>
{
    public IEnumerable<SwaggerExample<OpenAccountCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("National Currency Account", new OpenAccountCommand
        {
            CustomerId = 1,
            Currency = "GEL",
            Iban = "GE17646724343732516491"
        });

        yield return SwaggerExample.Create("Foreign Currency Account", new OpenAccountCommand
        {
            CustomerId = 1,
            Currency = "USD",
            Iban = "GE17646724343732516491"
        });
    }
}