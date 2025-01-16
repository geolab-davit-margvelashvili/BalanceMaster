using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceMaster.SqlRepository.EntityConfigurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        // Specify the table name (optional, defaults to the class name)
        builder.ToTable("Operations");

        // Primary key
        builder.HasKey(o => o.Id);

        // Properties
        builder.Property(o => o.AccountId)
            .IsRequired();

        builder.Property(o => o.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // Define precision and scale for the amount

        builder.Property(o => o.Currency)
            .IsRequired()
            .IsFixedLength()
            .IsUnicode(false)
            .HasMaxLength(3); // ISO 4217 currency code (e.g., USD, EUR)

        builder.Property(o => o.CreateAt)
            .IsRequired();

        builder.Property(o => o.OperationType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string for storage
                v => (OperationType)Enum.Parse(typeof(OperationType), v));

        // Relationships
        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(o => o.AccountId)
            .OnDelete(DeleteBehavior.Cascade); // Set cascading delete for account operations

        // Indexes
        builder.HasIndex(o => o.AccountId); // Index for faster lookups by AccountId
    }
}