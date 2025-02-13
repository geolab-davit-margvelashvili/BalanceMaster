using BalanceMaster.Identity.Models;
using BalanceMaster.Identity.Services.Abstractions;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _databaseContext;

    private readonly IUnitOfWork _unitOfWork;

    public TokenRepository(AppDbContext databaseContext, IUnitOfWork unitOfWork)
    {
        _databaseContext = databaseContext;
        _unitOfWork = unitOfWork;
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
        _unitOfWork.Start();

        await _databaseContext
            .RefreshTokens
            .AddAsync(refreshToken);

        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _unitOfWork.Start();

        _databaseContext
            .RefreshTokens
            .Attach(refreshToken);

        await _unitOfWork.CompleteAsync();
    }
}