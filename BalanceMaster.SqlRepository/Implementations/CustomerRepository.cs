using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _databaseContext;

    public CustomerRepository(AppDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await GetByIdOrDefaultAsync(id)
               ?? throw new ObjectNotFoundException(id.ToString(), nameof(Customer));
    }

    public Task<List<Customer>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByIdOrDefaultAsync(int id)
    {
        return await _databaseContext.Customers.FindAsync(id);
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