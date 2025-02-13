using BalanceMaster.Identity.Models;

namespace BalanceMaster.Identity.Services.Abstractions;

public interface ITokenRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token);

    Task DeleteRefreshToken(Guid id);

    Task AddRefreshTokenAsync(RefreshToken refreshToken);

    Task UpdateAsync(RefreshToken refreshToken);
}