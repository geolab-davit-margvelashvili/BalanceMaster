using BalanceMaster.Identity.Models;
using BalanceMaster.Identity.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _databaseContext;

    public TokenRepository(AppDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _databaseContext
            .RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == token);
    }

    public async Task DeleteRefreshToken(Guid id)
    {
        await _databaseContext
            .RefreshTokens
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _databaseContext
            .RefreshTokens
            .AddAsync(refreshToken);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _databaseContext
            .RefreshTokens
            .Attach(refreshToken);

        await _databaseContext.SaveChangesAsync();
    }
}