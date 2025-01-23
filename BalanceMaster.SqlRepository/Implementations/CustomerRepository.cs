using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _databaseContext;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerRepository(AppDbContext databaseContext, IUnitOfWork unitOfWork)
    {
        _databaseContext = databaseContext;
        _unitOfWork = unitOfWork;
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
        _unitOfWork.Start();
        await _databaseContext.Customers.AddAsync(customer);
        await _unitOfWork.CompleteAsync();
        return customer.Id;
    }

    public async Task UpdateAsync(Customer customer)
    {
        _unitOfWork.Start();
        _databaseContext.Attach(customer);
        await _unitOfWork.CompleteAsync();
    }
}