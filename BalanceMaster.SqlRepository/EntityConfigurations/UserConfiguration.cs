using BalanceMaster.SqlRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceMaster.SqlRepository.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasMaxLength(150);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);
    }
}