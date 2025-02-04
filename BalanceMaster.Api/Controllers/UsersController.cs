using BalanceMaster.Api.Extensions;
using BalanceMaster.SqlRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalanceMaster.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _singInManager;

    public UsersController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _singInManager = singInManager;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
        }

        var result = await _singInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return Ok(new
            {
                accessToken = GenerateJwt(user)
            });
        }

        return Unauthorized(result);
    }

    [HttpGet("confirm-email")]
    public async Task<ActionResult> ConfirmEmailAsync([FromQuery] string email, [FromQuery] string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound();
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            return Ok("Mail confirmed!");
        }

        return Unauthorized(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var uri = new UriBuilder("https", "localhost", 7017, $"api/users/confirm-email")
                {
                    Query = $"email={user.Email}&code={code}"
                };

                return Ok(new
                {
                    code = uri.ToString()
                });
            }

            return Ok(new
            {
                accessToken = GenerateJwt(user)
            });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized();
        }

        if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            return Unauthorized();
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded)
        {
            return Ok(new
            {
                accessToken = GenerateJwt(user)
            });
        }

        return BadRequest(result.Errors);
    }

    private string GenerateJwt(ApplicationUser user)
    {
        var securityKey = _configuration.GetIssuerSigningKey();
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new ("sub", user.Id),
            new ("preferred_username", user.UserName)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.GetJwtIssuer(),
            audience: _configuration.GetJwtAudience(),
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(10),
            signInCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return tokenToReturn;
    }
}

public class ChangePasswordRequest
{
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}