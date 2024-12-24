using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class
    AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IAccountRepository _repository;

    public AccountsController(IAccountService accountService, IAccountRepository repository)
    {
        _accountService = accountService;
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

    [HttpPost]
    public async Task<ActionResult> OpenAccount([FromBody] OpenAccountCommand command)
    {
        var id = await _accountService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetAccount), new { id }, new { id });
    }

    [HttpPut("{id}/close")]
    public async Task<ActionResult> CloseAccount([FromRoute] int id)
    {
        await _accountService.ExecuteAsync(new CloseAccountCommand { Id = id });
        return NoContent();
    }
}