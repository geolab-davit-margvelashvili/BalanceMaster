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
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsersController(IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (result.Succeeded)
        {
            return Ok(new
            {
                accessToken = GenerateJwt(user)
            });
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
            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                return Ok(new
                {
                    opt = code.Result
                });
            }

            return Ok(new
            {
                accessToken = GenerateJwt(user)
            });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Otp);
        if (!isValid)
        {
            return Unauthorized(new
            {
                message = "invalid otp"
            });
        }

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);

        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
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

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
        }

        var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "ResetPassword");

        return Ok(new
        {
            resetToken = otp
        });
    }

    [HttpPost("new-password")]
    public async Task<ActionResult> NewPasswordAsync([FromBody] NewPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp);
        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid OTP" });
        }

        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return StatusCode(500, new { message = "Failed to remove the old password" });
            }
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            return StatusCode(500, new { message = "Failed to set the new password", errors = addPasswordResult.Errors });
        }

        return Ok(new { message = "Password has been reset successfully." });
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

public class ConfirmEmailRequest
{
    public required string Email { get; set; }
    public required string Otp { get; set; }
}

public class ResetPasswordRequest
{
    public required string Email { get; set; }
}

public class NewPasswordRequest
{
    public required string Email { get; set; }
    public required string NewPassword { get; set; }
    public required string Otp { get; set; }
}