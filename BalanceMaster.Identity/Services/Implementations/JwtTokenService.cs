using BalanceMaster.Identity.Exceptions;
using BalanceMaster.Identity.Models;
using BalanceMaster.Identity.Services.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalanceMaster.Identity.Services.Implementations;

public sealed class JwtTokenService : ITokenService
{
    private readonly TokenServiceOptions _options;
    private readonly ITokenRepository _tokenRepository;

    public JwtTokenService(IOptions<TokenServiceOptions> options, ITokenRepository tokenRepository)
    {
        _options = options.Value;
        _tokenRepository = tokenRepository;
    }

    public string GenerateAccessTokenFor(ApplicationUser user)
    {
        var signInCredentials = new SigningCredentials(_options.GetIssuerSigningKey(), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new ("sub", user.Id),
            new ("preferred_username", user.UserName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(1),
            signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
    {
        var refreshToken = RefreshToken.CreateNew(user.Id);
        await _tokenRepository.AddRefreshTokenAsync(refreshToken);
        return refreshToken.Value;
    }

    public async Task<string> RefreshTokenAsync(string token)
    {
        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(token);
        if (refreshToken is null)
        {
            throw new AuthenticationException("Invalid refresh token");
        }

        if (refreshToken.ExpiresAt <= DateTime.Now)
        {
            throw new AuthenticationException("Refresh token has expired");
        }

        refreshToken.Refresh();
        await _tokenRepository.UpdateAsync(refreshToken);

        return refreshToken.Value;
    }
}