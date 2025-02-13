using BalanceMaster.Identity.Requests;
using BalanceMaster.Identity.Responses;

namespace BalanceMaster.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest request);

    Task<LoginResponse?> RegisterAsync(RegisterRequest request);

    Task ConfirmEmailAsync(ConfirmEmailRequest request);

    Task<LoginResponse> ChangePasswordAsync(ChangePasswordRequest request);

    Task ResetPasswordAsync(ResetPasswordRequest request);

    Task<LoginResponse> NewPasswordAsync(NewPasswordRequest request);
}