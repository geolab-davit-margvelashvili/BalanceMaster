using BalanceMaster.Identity.Requests;
using BalanceMaster.Identity.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync([FromBody] LoginRequest request)
    {
        return Ok(new
        {
            accessToken = await _identityService.AuthenticateAsync(request)
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var accessToken = await _identityService.RegisterAsync(request);

        if (accessToken is not null)
        {
            return Ok(new
            {
                accessToken
            });
        }

        return Ok();
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        await _identityService.ConfirmEmail(request);
        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var accessToken = await _identityService.ChangePasswordAsync(request);

        return Ok(new
        {
            accessToken
        });
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
    {
        await _identityService.ResetPasswordAsync(request);
        return Ok();
    }

    [HttpPost("new-password")]
    public async Task<ActionResult> NewPasswordAsync([FromBody] NewPasswordRequest request)
    {
        await _identityService.NewPasswordAsync(request);
        return Ok();
    }
}