using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class LoginRequestExamples : IMultipleExamplesProvider<LoginRequest>
{
    public IEnumerable<SwaggerExample<LoginRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Login", new LoginRequest
        {
            Email = "test@yopmail.com",
            Password = "P@$$w0rd"
        });
    }
}