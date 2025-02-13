using BalanceMaster.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceMaster.SqlRepository.EntityConfigurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(r => r.Value)
            .HasMaxLength(200);

        builder
            .HasIndex(r => r.Value)
            .IsUnique();

        builder
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}