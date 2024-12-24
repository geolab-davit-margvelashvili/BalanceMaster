using BalanceMaster.Domain.Models;
using BalanceMaster.Domain.Queries;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BalanceMaster.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public AccountsController(IAccountRepository repository, IOptionsSnapshot<DatabaseOptions> options)
    {
        _repository = repository;

        var systemDatabaseOptions = options.Get(DatabaseOptions.SystemDatabaseSectionName);
        var businessDatabaseOptions = options.Get(DatabaseOptions.BusinessDatabaseSectionName);
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