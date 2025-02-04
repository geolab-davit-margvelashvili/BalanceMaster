using Microsoft.AspNetCore.Identity;

namespace BalanceMaster.Api;

public sealed class PasswordTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser> where TUser : IdentityUser
{
    public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
    }

    public override async Task<string> GetUserModifierAsync(
        string purpose,
        UserManager<TUser> manager,
        TUser user)
    {
        return "Password:" + purpose + ":" + await manager.GetUserIdAsync(user).ConfigureAwait(false);
    }
}