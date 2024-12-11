using BalanceMaster.Service.Models;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public AccountsController(IAccountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public async Task<Account> GetAccount(int id)
    {
        var account = await _repository.GetByIdAsync(id);
        return account;
    }

    [HttpGet]
    public async Task<List<Account>> ListAccounts([FromQuery] decimal? balance)
    {
        var account = await _repository.ListAsync();

        if (balance is not null)
        {
            return account.Where(x => x.Balance == balance.Value).ToList();
        }

        return account;
    }

    [HttpGet("ListWithOverdrafts")]
    public async Task<List<Account>> ListAccountsWithOverdraft()
    {
        var account = await _repository.ListAsync();
        return account.Where(x => x.Overdraft is not null).ToList();
    }

    [HttpGet("CreditAccount")]
    public Task GetCreditAccount()
    {
        return Task.CompletedTask;
    }

    [HttpPost("CreditAccount")]
    public Task CreateCreditAccount()
    {
        return Task.CompletedTask;
    }
}