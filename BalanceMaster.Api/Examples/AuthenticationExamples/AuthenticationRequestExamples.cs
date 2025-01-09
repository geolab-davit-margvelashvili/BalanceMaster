using BalanceMaster.Api.Controllers;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.AuthenticationExamples;

public sealed class AuthenticationRequestExamples : IMultipleExamplesProvider<AuthenticationRequest>
{
    public IEnumerable<SwaggerExample<AuthenticationRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Login", new AuthenticationRequest
        {
            UserName = "test@mail.com",
            Password = "123"
        });
    }
}