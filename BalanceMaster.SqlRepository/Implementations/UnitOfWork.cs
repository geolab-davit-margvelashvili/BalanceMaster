using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Database;

namespace BalanceMaster.SqlRepository.Implementations;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private int _counter = 0;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        _counter = 0;
    }

    public void Start()
    {
        _counter++;
    }

    public async Task CompleteAsync()
    {
        if (_counter == 0)
            return;

        if (_counter == 1)
        {
            await _dbContext.SaveChangesAsync();
        }
        _counter--;
    }
}