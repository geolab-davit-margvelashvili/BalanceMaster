using BalanceMaster.Api.Models;
using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Domain.Models;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<ActionResult> ListOperation([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var pagedData = await _operationRepository.ListAsync(page, pageSize);

        var response = new ApiPagedResponse<Operation>
        {
            Data = pagedData.Data.ToList(),
            Meta = new Meta
            {
                Page = pagedData.Page,
                PageSize = pagedData.PageSize,
                TotalItems = pagedData.TotalItems,
                TotalPages = pagedData.TotalPages
            },
            Links = new Links
            {
                Self = CreateLink(page, pageSize),
                Next = page >= pagedData.TotalPages ? null : CreateLink(page + 1, pageSize),
                Prev = page == 1 ? null : CreateLink(page - 1, pageSize)
            }
        };

        return Ok(response);
    }

    [Authorize]
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

    private string? CreateLink(int page, int pageSize) =>
        Url.Action(
            action: "ListOperation",
            controller: "Operations",
            values: new { page, pageSize },
            protocol: Request.Scheme,
            host: Request.Host.Value
        );
}