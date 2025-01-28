using BalanceMaster.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ReserveConfiguration : IEntityTypeConfiguration<Reserve>
{
    public void Configure(EntityTypeBuilder<Reserve> builder)
    {
        builder.ToTable("Reserves");

        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.AccountId)
            .IsRequired();

        builder.Property(a => a.Currency)
            .IsRequired()
            .IsFixedLength()
            .IsUnicode(false)
            .HasMaxLength(3); // ISO 4217 currency code (e.g., USD, EUR)

        builder.Property(a => a.Description)
            .IsUnicode()
            .HasMaxLength(200);

        builder.Property(a => a.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}