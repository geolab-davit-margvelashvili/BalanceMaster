using BalanceMaster.Identity.Requests;

namespace BalanceMaster.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<string> AuthenticateAsync(LoginRequest request);

    Task<string?> RegisterAsync(RegisterRequest request);

    Task ConfirmEmail(ConfirmEmailRequest request);

    Task<string> ChangePasswordAsync(ChangePasswordRequest request);

    Task ResetPasswordAsync(ResetPasswordRequest request);

    Task NewPasswordAsync(NewPasswordRequest request);
}