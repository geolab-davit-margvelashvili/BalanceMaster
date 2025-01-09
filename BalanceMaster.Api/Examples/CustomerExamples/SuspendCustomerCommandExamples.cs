using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.CustomerExamples;

public sealed class SuspendCustomerCommandExamples : IMultipleExamplesProvider<SuspendCustomerCommand>
{
    public IEnumerable<SwaggerExample<SuspendCustomerCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Suspend Customer", new SuspendCustomerCommand
        {
            CustomerId = 1
        });
    }
}