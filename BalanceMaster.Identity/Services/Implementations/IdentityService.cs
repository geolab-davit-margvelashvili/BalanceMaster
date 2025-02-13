using BalanceMaster.Identity.Exceptions;
using BalanceMaster.Identity.Models;
using BalanceMaster.Identity.Requests;
using BalanceMaster.Identity.Responses;
using BalanceMaster.Identity.Services.Abstractions;
using BalanceMaster.MessageSender.Abstractions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BalanceMaster.Identity.Services.Implementations;

public sealed class IdentityService : IIdentityService
{
    #region Fields

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<IdentityService> _logger;

    #endregion Fields

    #region Ctor

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IEmailSender emailSender,
        ILogger<IdentityService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailSender = emailSender;
        _logger = logger;
    }

    #endregion Ctor

    #region Public Methods

    public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (!result.Succeeded)
        {
            _logger.LogWarning("login failed. result: {@Result}", result);
            throw new AuthenticationException();
        }

        return await CreateLoginResponseAsync(user);
    }

    public async Task<LoginResponse?> RegisterAsync(RegisterRequest request)
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
                await SendOtpAsync(request.Email, code.Result);
                return null;
            }

            return await CreateLoginResponseAsync(user);
        }

        throw new IdentityException(result.Errors);
    }

    public async Task ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Otp);
        if (!isValid)
        {
            throw new AuthenticationException();
        }

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
    }

    public async Task<LoginResponse> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            throw new AuthenticationException();
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded)
        {
            return await CreateLoginResponseAsync(user);
        }

        throw new IdentityException(result.Errors);
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "ResetPassword");
        await SendOtpAsync(request.Email, otp);
    }

    public async Task<LoginResponse> NewPasswordAsync(NewPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp);
        if (!isValid)
        {
            throw new AuthenticationException();
        }

        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                throw new ChangePasswordException(removePasswordResult.Errors, "Failed to remove the old password");
            }
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            throw new ChangePasswordException(addPasswordResult.Errors, "Failed to remove the old password");
        }

        return await CreateLoginResponseAsync(user);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<LoginResponse> CreateLoginResponseAsync(ApplicationUser user)
    {
        return new LoginResponse
        {
            AccessToken = _tokenService.GenerateAccessTokenFor(user),
            RefreshToken = await _tokenService.GenerateRefreshTokenAsync(user),
        };
    }

    private async Task SendOtpAsync(string email, string otp)
    {
        await _emailSender.SendEmailAsync(email, subject: "Otp", otp);
    }

    #endregion Private Methods
}