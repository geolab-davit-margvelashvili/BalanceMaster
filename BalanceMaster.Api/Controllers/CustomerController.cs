using BalanceMaster.Domain.Abstractions;
using BalanceMaster.Domain.Commands;
using BalanceMaster.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BalanceMaster.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository)
    {
        _customerService = customerService;
        _customerRepository = customerRepository;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customerId = await _customerService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, null);
    }

    [HttpPost("{id}/open")]
    public async Task<IActionResult> OpenCustomer([FromRoute] int id, [FromBody] OpenCustomerCommand command)
    {
        if (id != command.CustomerId)
        {
            return BadRequest();
        }

        await _customerService.ExecuteAsync(command);
        return NoContent();
    }

    [HttpPost("{id}/close")]
    public async Task<IActionResult> CloseCustomer([FromRoute] int id, [FromBody] CloseCustomerCommand command)
    {
        if (id != command.CustomerId)
        {
            return BadRequest();
        }

        await _customerService.ExecuteAsync(command);
        return NoContent();
    }

    [HttpPost("{id}/suspend")]
    public async Task<IActionResult> SuspendCustomer([FromRoute] int id, [FromBody] SuspendCustomerCommand command)
    {
        if (id != command.CustomerId)
        {
            return BadRequest();
        }

        await _customerService.ExecuteAsync(command);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
        var customer = await _customerRepository.GetByIdOrDefaultAsync(id);
        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}