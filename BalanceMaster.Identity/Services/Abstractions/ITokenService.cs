using BalanceMaster.Identity.Models;

namespace BalanceMaster.Identity.Services.Abstractions;

public interface ITokenService
{
    string GenerateTokenFor(ApplicationUser user);
}