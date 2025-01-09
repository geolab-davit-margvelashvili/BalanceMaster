using BalanceMaster.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalanceMaster.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody request)
    {
        // Step 1: Validate the username/password
        var user = ValidateCredentials(request.UserName, request.Password);

        if (user is null)
        {
            return Unauthorized();
        }

        // Step 2: create token
        var securityKey = _configuration.GetIssuerSigningKey();
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new ("sub", user.Id.ToString()),
            new ("given_name", user.FirstName),
            new ("family_name", user.LastName),
            new ("preferred_username", user.UserName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetJwtIssuer(),
                audience: _configuration.GetJwtAudience(),
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signInCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return Ok(tokenToReturn);
    }

    private ApplicationUser? ValidateCredentials(string? requestUserName, string? requestPassword)
    {
        return new ApplicationUser()
        {
            Id = 1,
            FirstName = "First",
            LastName = "Last",
            UserName = "test@mail.com"
        };
    }
}

public sealed class ApplicationUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
}

public sealed class AuthenticationRequestBody
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}