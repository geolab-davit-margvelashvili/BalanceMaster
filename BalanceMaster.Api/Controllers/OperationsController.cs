using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/operations")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;

    public OperationsController(IOperationService operationService)
    {
        _operationService = operationService;
    }

    [HttpPost("credit")]
    public async Task<ActionResult> Credit(CreditCommand command)
    {
        await _operationService.ExecuteAsync(command);
        return Ok();
    }

    [HttpPost("debit")]
    public async Task<ActionResult> Debit(DebitCommand command)
    {
        await _operationService.ExecuteAsync(command);
        return Ok();
    }
}