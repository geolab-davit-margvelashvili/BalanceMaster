using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceMaster.SqlRepository.EntityConfigurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Specify the table name (optional, defaults to the class name)
        builder.ToTable("Customers");

        // Primary key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100); // Set maximum length for FirstName

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100); // Set maximum length for LastName

        builder.Property(c => c.PrivateNumber)
            .IsRequired()
            .IsFixedLength()
            .IsUnicode(false)
            .HasMaxLength(11); // Set maximum length for PrivateNumber

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(), // Convert enum to string for storage
                v => (CustomerStatus)Enum.Parse(typeof(CustomerStatus), v));

        // Indexes
        builder
            .HasIndex(c => c.PrivateNumber)
            .IsUnique(); // Ensure PrivateNumber is unique
    }
}