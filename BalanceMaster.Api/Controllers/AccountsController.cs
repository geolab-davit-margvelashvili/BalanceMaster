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
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(IAccountService accountService, IAccountRepository repository, ILogger<AccountsController> logger)
    {
        _accountService = accountService;
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(
        [FromRoute(Name = "id")] int accountId,
        [FromQuery(Name = "active")] bool isActive,
        [FromQuery(Name = "withReserves")] bool withReserves)
    {
        _logger.LogInformation("Searching account with id: {AccountId}", accountId);

        var account = await _repository.GetByIdOrDefaultAsync(accountId, withReserves);
        if (account is not null)
        {
            _logger.LogDebug("Account found: {@Account}", account);
            return Ok(account);
        }

        _logger.LogWarning("Account not found");
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

    [HttpPost("{id}/reserves")]
    public async Task<ActionResult> AddReserve([FromRoute(Name = "id")] int accountId, [FromBody] AddReserveCommand command)
    {
        if (accountId != command.AccountId)
            return BadRequest("Account id mismatch");

        await _accountService.ExecuteAsync(command);
        return NoContent();
    }
}