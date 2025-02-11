namespace BalanceMaster.Identity.Requests;

public sealed class ConfirmEmailRequest
{
    public required string Email { get; set; }
    public required string Otp { get; set; }
}