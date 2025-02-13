namespace BalanceMaster.Identity.Responses;

public sealed class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}