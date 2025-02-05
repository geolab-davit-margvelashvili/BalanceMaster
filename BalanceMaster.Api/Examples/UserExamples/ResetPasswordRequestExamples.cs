using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class ResetPasswordRequestExamples : IMultipleExamplesProvider<ResetPasswordRequest>
{
    public IEnumerable<SwaggerExample<ResetPasswordRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Reset Password", new ResetPasswordRequest
        {
            Email = "test@yopmail.com",
        });
    }
}