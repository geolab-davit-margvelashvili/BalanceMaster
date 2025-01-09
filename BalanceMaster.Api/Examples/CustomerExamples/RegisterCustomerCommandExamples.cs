using BalanceMaster.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.CustomerExamples;

public sealed class RegisterCustomerCommandExamples : IMultipleExamplesProvider<RegisterCustomerCommand>
{
    public IEnumerable<SwaggerExample<RegisterCustomerCommand>> GetExamples()
    {
        yield return SwaggerExample.Create("Register Customer", new RegisterCustomerCommand
        {
            FirstName = "First",
            LastName = "Last",
            PrivateNumber = "01010101100"
        });
    }
}