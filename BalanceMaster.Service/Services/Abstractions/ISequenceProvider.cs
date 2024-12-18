namespace BalanceMaster.Service.Services.Abstractions;

public interface ISequenceProvider
{
    Task<int> GetNextInteger(string sequenceName);

    Task<long> GetNextBigInteger(string sequenceName);
}