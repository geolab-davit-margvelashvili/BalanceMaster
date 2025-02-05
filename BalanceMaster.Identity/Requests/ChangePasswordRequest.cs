namespace BalanceMaster.Identity.Requests;

public sealed class ChangePasswordRequest
{
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}