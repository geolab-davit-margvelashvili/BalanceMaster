using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;

namespace BalanceMaster.Service.Services.Implementations.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> ExecuteAsync(RegisterCustomerCommand command)
    {
        command.Validate();
        var customer = new Customer(command.FirstName, command.LastName, command.PrivateNumber);
        return await _customerRepository.CreateAsync(customer);
    }

    public async Task ExecuteAsync(OpenCustomerCommand command)
    {
        command.Validate();

        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        customer.Open();
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task ExecuteAsync(CloseCustomerCommand command)
    {
        command.Validate();
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        customer.Close();
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task ExecuteAsync(SuspendCustomerCommand command)
    {
        command.Validate();
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
        customer.Suspend();
        await _customerRepository.UpdateAsync(customer);
    }
}