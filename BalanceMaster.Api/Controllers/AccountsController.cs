using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Models;
using BalanceMaster.Service.Queries;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public AccountsController(IAccountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount([FromRoute(Name = "id")] int accountId, [FromQuery(Name = "active")] bool isActive)
    {
        var account = await _repository.GetByIdOrDefaultAsync(accountId);
        if (account is not null)
            return Ok(account);

        return NotFound();
    }

    [HttpGet]
    public async Task<List<Account>> ListAccounts([FromQuery] AccountQueryFilter? filter)
    {
        var account = await _repository.ListAsync(filter);

        //if (balance is not null)
        //{
        //    return account.Where(x => x.Balance == balance.Value).ToList();
        //}

        return account;
    }

    [HttpGet("list-with-overdrafts")]
    public async Task<List<Account>> ListAccountsWithOverdraft()
    {
        var account = await _repository.ListAsync(null);
        return account.Where(x => x.Overdraft is not null).ToList();
    }

    [HttpGet("credit-account")]
    public Task GetCreditAccount()
    {
        return Task.CompletedTask;
    }

    [HttpPost("credit-account")]
    public Task CreateCreditAccount([FromBody] OpenAccountCommand command)
    {
        return Task.CompletedTask;
    }
}