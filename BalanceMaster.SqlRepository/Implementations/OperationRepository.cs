using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

internal sealed class OperationRepository : IOperationRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public OperationRepository(AppDbContext appDbContext, IUnitOfWork unitOfWork)
    {
        _appDbContext = appDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(Operation operation)
    {
        _unitOfWork.Start();
        await _appDbContext.Operations.AddAsync(operation);
        await _unitOfWork.CompleteAsync();
        return operation.Id;
    }

    public async Task<Operation> GetByIdAsync(Guid id)
    {
        return await GetByIdOrDefaultAsync(id)
               ?? throw new ObjectNotFoundException(id.ToString(), nameof(Operation));
    }

    public async Task<Operation?> GetByIdOrDefaultAsync(Guid id)
    {
        return await _appDbContext.Operations.FindAsync(id);
    }
}