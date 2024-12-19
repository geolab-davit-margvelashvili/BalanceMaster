using BalanceMaster.Domain.Abstractions;
using System.Collections.Concurrent;
using System.Text.Json;

namespace BalanceMaster.Service.Services.Implementations.FileRepositories;

public sealed class FileSequenceProvider : ISequenceProvider
{
    private const string FilePath = "sequences.json";

    private static readonly ConcurrentDictionary<string, long>? Sequences = new();

    static FileSequenceProvider()
    {
        if (File.Exists(FilePath))
        {
            var content = File.ReadAllText(FilePath);
            Sequences = JsonSerializer.Deserialize<ConcurrentDictionary<string, long>>(content) ?? new();
        }
        else
        {
            Sequences = new();
            SaveChanges();
        }
    }

    private static void SaveChanges()
    {
        File.WriteAllText(FilePath, JsonSerializer.Serialize(Sequences));
    }

    public Task<int> GetNextInteger(string sequenceName)
    {
        if (string.IsNullOrEmpty(sequenceName))
            throw new ArgumentException("Sequence name cannot be null or empty.", nameof(sequenceName));

        if (Sequences == null)
            throw new InvalidOperationException("Sequence is not initialized");

        var nextValue = Sequences.AddOrUpdate(sequenceName, 1, (key, currentValue) => currentValue + 1);
        SaveChanges();

        return Task.FromResult((int)nextValue);
    }

    public Task<long> GetNextBigInteger(string sequenceName)
    {
        if (string.IsNullOrEmpty(sequenceName))
            throw new ArgumentException("Sequence name cannot be null or empty.", nameof(sequenceName));

        if (Sequences == null)
            throw new InvalidOperationException("Sequence is not initialized");

        var nextValue = Sequences.AddOrUpdate(sequenceName, 1, (key, currentValue) => currentValue + 1);
        SaveChanges();

        return Task.FromResult(nextValue);
    }
}