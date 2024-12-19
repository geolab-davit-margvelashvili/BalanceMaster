namespace BalanceMaster.Domain.Abstractions;

public interface ISequenceProvider
{
    Task<int> GetNextInteger(string sequenceName);

    Task<long> GetNextBigInteger(string sequenceName);
}