using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PagedResponse<Operation>> ListAsync(int page, int pageSize)
    {
        var data = await _appDbContext
            .Operations
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await _appDbContext.Operations.CountAsync();

        var response = new PagedResponse<Operation>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            TotalPages = (int)Math.Ceiling((double)total / pageSize)
        };

        return response;
    }

    public async Task<Operation?> GetByIdOrDefaultAsync(Guid id)
    {
        return await _appDbContext.Operations.FindAsync(id);
    }
}