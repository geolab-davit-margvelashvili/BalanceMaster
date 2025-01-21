using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/operations")]
[Authorize]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;

    public OperationsController(IOperationService operationService)
    {
        _operationService = operationService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Operation>> GetOperation(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("credit")]
    public async Task<ActionResult> Credit(CreditCommand command)
    {
        var id = await _operationService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetOperation), new { id }, new { id });
    }

    [HttpPost("debit")]
    public async Task<ActionResult> Debit(DebitCommand command)
    {
        var id = await _operationService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetOperation), new { id }, new { id });
    }
}