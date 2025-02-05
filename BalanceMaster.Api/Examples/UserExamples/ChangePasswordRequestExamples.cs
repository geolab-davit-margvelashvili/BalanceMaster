using BalanceMaster.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace BalanceMaster.Api.Examples.UserExamples;

public sealed class ChangePasswordRequestExamples : IMultipleExamplesProvider<ChangePasswordRequest>
{
    public IEnumerable<SwaggerExample<ChangePasswordRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Change Password (P@$$w0rd -> P@$$w0rd1234)", new ChangePasswordRequest
        {
            Email = "test@yopmail.com",
            CurrentPassword = "P@$$w0rd",
            NewPassword = "P@$$w0rd1234"
        });

        yield return SwaggerExample.Create("Change Password (P@$$w0rd1234 -> P@$$w0rd)", new ChangePasswordRequest
        {
            Email = "test@yopmail.com",
            CurrentPassword = "P@$$w0rd1234",
            NewPassword = "P@$$w0rd",
        });
    }
}