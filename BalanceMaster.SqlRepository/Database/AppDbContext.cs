using BalanceMaster.Domain.Models;
using BalanceMaster.SqlRepository.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BalanceMaster.SqlRepository.Database;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Operation> Operations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new OperationConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveConfiguration());
    }
}