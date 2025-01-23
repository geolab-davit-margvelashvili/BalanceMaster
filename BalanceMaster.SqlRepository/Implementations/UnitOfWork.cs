using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

public sealed class UnitOfWork : IUnitOfWork
{
    private int _count = 0;

    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Start()
    {
        _count++;
    }

    public async Task CompleteAsync()
    {
        if (_count == 0)
        {
            return;
        }

        if (_count == 1)
        {
            await _appDbContext.SaveChangesAsync();
        }

        _count--;
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
            _count = 0;
        }
    }

    public ValueTask DisposeAsync()
    {
        _count = 0;
        return ValueTask.CompletedTask;
    }
}