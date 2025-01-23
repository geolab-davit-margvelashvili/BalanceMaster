using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Specify the table name (optional, defaults to the class name)
        builder.ToTable("Accounts");

        // Primary key
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.CustomerId)
            .IsRequired();

        builder.Property(a => a.Iban)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(34); // IBAN maximum length is typically 34 characters

        builder.Property(a => a.Currency)
            .IsRequired()
            .IsFixedLength()
            .IsUnicode(false)
            .HasMaxLength(3); // ISO 4217 currency code (e.g., USD, EUR)

        builder.Property(a => a.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); // Define precision and scale for the balance

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string for storage
                v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v));

        builder
            .OwnsOne(a => a.Overdraft)
            .ToTable("Overdrafts");

        // Indexes
        builder.HasIndex(a => a.Iban).IsUnique(); // Ensure IBAN is unique
        builder.HasIndex(a => a.Currency);

        // Relationships
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Set cascading delete for customer accounts
    }
}