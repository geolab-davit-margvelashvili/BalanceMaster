using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.CustomerExamples;

public sealed class CloseCustomerCommandExamples : IMultipleExamplesProvider<CloseCustomerCommand>
{
    public IEnumerable<SwaggerExample<CloseCustomerCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Close Customer", new CloseCustomerCommand
        {
            CustomerId = 1
        });
    }
}