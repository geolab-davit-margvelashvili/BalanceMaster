namespace BalanceMaster.Identity.Requests;

public sealed class ResetPasswordRequest
{
    public required string Email { get; set; }
}