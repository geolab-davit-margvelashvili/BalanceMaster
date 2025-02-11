namespace BalanceMaster.Identity.Requests;

public sealed class RegisterRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}