using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

internal class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _databaseContext;

    public CustomerRepository(AppDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<Customer> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdOrDefaultAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(Customer customer)
    {
        await _databaseContext.Customers.AddAsync(customer);
        await _databaseContext.SaveChangesAsync();
        return customer.Id;
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}