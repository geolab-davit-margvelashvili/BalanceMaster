using Microsoft.AspNetCore.Identity.Data;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class RegisterRequestExamples : IMultipleExamplesProvider<RegisterRequest>
{
    public IEnumerable<SwaggerExample<RegisterRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Login", new RegisterRequest
        {
            Email = $"test-{Guid.NewGuid()}@mail.com",
            Password = "P@$$w0rd"
        });
    }
}