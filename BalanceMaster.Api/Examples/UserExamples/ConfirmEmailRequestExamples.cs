using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class ConfirmEmailRequestExamples : IMultipleExamplesProvider<ConfirmEmailRequest>
{
    public IEnumerable<SwaggerExample<ConfirmEmailRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Confirm Email", new ConfirmEmailRequest
        {
            Email = "test@yopmail.com",
            Otp = "123456"
        });
    }
}