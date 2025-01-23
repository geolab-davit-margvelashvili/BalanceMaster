using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.Api.Controllers;

[Route("api/operations")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;
    private readonly IOperationRepository _operationRepository;

    public OperationsController(IOperationService operationService, IOperationRepository operationRepository)
    {
        _operationService = operationService;
        _operationRepository = operationRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Operation>> GetOperation(Guid id)
    {
        return await _operationRepository.GetByIdAsync(id);
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