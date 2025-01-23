using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BalanceMaster.SqlRepository.Implementations;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _transaction?.Dispose();
        }
    }

    public async Task RollBackAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
        }
    }

    public async Task CommitAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync();
        }
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
        }
    }
}

public sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly AppDbContext _dbContext;

    public UnitOfWorkFactory(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IUnitOfWork> StartUnitOfWorkAsync()
    {
        var unitOfWork = new UnitOfWork(_dbContext);
        await unitOfWork.BeginTransactionAsync();
        return unitOfWork;
    }
}