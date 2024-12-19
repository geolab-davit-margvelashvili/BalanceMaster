using BalanceMaster.Domain.Models;

namespace BalanceMaster.Domain.Abstractions;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id);

    Task<List<Customer>> ListAsync();

    Task<Customer?> GetByIdOrDefaultAsync(int id);

    Task<int> CreateAsync(Customer customer);

    Task UpdateAsync(Customer customer);
}