using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class NewPasswordRequestExamples : IMultipleExamplesProvider<NewPasswordRequest>
{
    public IEnumerable<SwaggerExample<NewPasswordRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("New Password", new NewPasswordRequest
        {
            Email = "test@yopmail.com",
            NewPassword = "P@$$w0rd1234",
            Otp = "123456"
        });
    }
}