using BalanceMaster.Service.Exceptions;
using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;
using System.Text.Json;

namespace BalanceMaster.Service.Services.Implementations;

public sealed class FileAccountRepository : IAccountRepository
{
    private readonly string _filePath;
    private readonly List<Account> _accounts;

    public FileAccountRepository()
    {
        _filePath = "accounts.json";
        _accounts = LoadAccountsFromFile();
    }

    private List<Account> LoadAccountsFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Account>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
    }

    private void SaveAccountsToFile()
    {
        var json = JsonSerializer.Serialize(_accounts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        var account = await GetByIdOrDefaultAsync(id);
        if (account is null)
        {
            throw new ObjectNotFoundException(id.ToString(), nameof(Account));
        }

        return account;
    }

    public Task<List<Account>> ListAsync(QueryFilter? filter)
    {
        return Task.FromResult(_accounts);
    }

    public Task<Account?> GetByIdOrDefaultAsync(int id)
    {
        var account = _accounts.FirstOrDefault(account => account.Id == id);
        return Task.FromResult(account);
    }

    public Task SaveAccountAsync(Account account)
    {
        var existingAccount = _accounts.FirstOrDefault(a => a.Id == account.Id);
        if (existingAccount != null)
        {
            _accounts.Remove(existingAccount);
        }

        _accounts.Add(account);
        SaveAccountsToFile();

        return Task.CompletedTask;
    }
}