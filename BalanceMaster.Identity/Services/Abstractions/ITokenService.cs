using BalanceMaster.Identity.Models;

namespace BalanceMaster.Identity.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessTokenFor(ApplicationUser user);

    Task<string> GenerateRefreshTokenAsync(ApplicationUser user);

    Task<string> RefreshTokenAsync(string token);
}