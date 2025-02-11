namespace BalanceMaster.Identity.Requests;

public sealed class NewPasswordRequest
{
    public required string Email { get; set; }
    public required string NewPassword { get; set; }
    public required string Otp { get; set; }
}