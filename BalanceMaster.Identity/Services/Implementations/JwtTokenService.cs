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

    public JwtTokenService(IOptions<TokenServiceOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateTokenFor(ApplicationUser user)
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
            expires: DateTime.UtcNow.AddMinutes(5),
            signInCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}