using System.Security.Cryptography;

namespace BalanceMaster.Identity.Models;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public string UserId { get; private set; } = string.Empty;

    public ApplicationUser? User { get; private set; }

    private RefreshToken()
    {
    }

    public static RefreshToken CreateNew(string userId)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            ExpiresAt = DateTime.Now.AddDays(15),
            UserId = userId,
            Value = GenerateNewValue()
        };
    }

    public void Refresh()
    {
        Value = GenerateNewValue();
    }

    private static string GenerateNewValue() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}