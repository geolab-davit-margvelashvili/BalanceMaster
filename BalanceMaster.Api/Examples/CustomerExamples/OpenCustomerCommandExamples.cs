using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.CustomerExamples;

public sealed class OpenCustomerCommandExamples : IMultipleExamplesProvider<OpenCustomerCommand>
{
    public IEnumerable<SwaggerExample<OpenCustomerCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Open Customer", new OpenCustomerCommand
        {
            CustomerId = 1
        });
    }
}