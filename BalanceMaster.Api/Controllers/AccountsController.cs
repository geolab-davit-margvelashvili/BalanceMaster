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
    public async Task<ActionResult<List<Account>>> ListAccounts([FromQuery] AccountQueryFilter? filter) =>
        await _repository.ListAsync(filter);
}