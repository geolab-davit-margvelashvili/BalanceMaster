using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class RegisterRequestExamples : IMultipleExamplesProvider<RegisterRequest>
{
    public IEnumerable<SwaggerExample<RegisterRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Register (test)", new RegisterRequest
        {
            Email = "test@yopmail.com",
            Password = "P@$$w0rd"
        });

        yield return SwaggerExample.Create("Register (test 1)", new RegisterRequest
        {
            Email = "test1@yopmail.com",
            Password = "P@$$w0rd"
        });

        yield return SwaggerExample.Create("Register (test 2)", new RegisterRequest
        {
            Email = "test2@yopmail.com",
            Password = "P@$$w0rd"
        });

        yield return SwaggerExample.Create("Register (test 3)", new RegisterRequest
        {
            Email = "test3@yopmail.com",
            Password = "P@$$w0rd"
        });

        yield return SwaggerExample.Create("Register (random mail)", new RegisterRequest
        {
            Email = $"test-{Guid.NewGuid()}@yopmail.com",
            Password = "P@$$w0rd"
        });
    }
}