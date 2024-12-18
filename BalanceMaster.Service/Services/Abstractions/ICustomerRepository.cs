using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id);

    Task<List<Customer>> ListAsync();

    Task<Customer?> GetByIdOrDefaultAsync(int id);

    Task<int> CreateAsync(Customer customer);

    Task UpdateAsync(Customer customer);
}