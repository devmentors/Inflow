using System;
using System.Threading.Tasks;
using Inflow.Modules.Customers.Core.Commands;
using Inflow.Modules.Customers.Core.DTO;
using Inflow.Modules.Customers.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Contexts;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace Inflow.Modules.Customers.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal class CustomersController : Controller
{
    private const string Policy = "customers";
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public CustomersController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }
        
    [HttpGet]
    [Authorize(Policy)]
    [SwaggerOperation("Browse customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Paged<CustomerDetailsDto>>> BrowseAsync([FromQuery] BrowseCustomers query)
        => Ok(await _dispatcher.QueryAsync(query));

    [HttpGet("{customerId:guid}")]
    [Authorize]
    [SwaggerOperation("Get customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CustomerDetailsDto>> GetAsync(Guid customerId)
    {
        // Customer cannot access the other customer accounts
        customerId = _context.Identity.IsUser() ? _context.Identity.Id : customerId;
        var customer = await _dispatcher.QueryAsync(new GetCustomer { CustomerId = customerId });
        if (customer is not null)
        {
            return Ok(customer);
        }

        return NotFound();
    }

    [HttpPut("complete")]
    [Authorize]
    [SwaggerOperation("Complete customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Post(CompleteCustomer command)
    {
        await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, _context.Identity.Id));
        return NoContent();
    }
        
    [HttpPut("{customerId:guid}/verify")]
    [Authorize(Policy)]
    [SwaggerOperation("Verify customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(Guid customerId, VerifyCustomer command)
    {
        await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
        return NoContent();
    }
        
    [HttpPut("{customerId:guid}/lock")]
    [Authorize(Policy)]
    [SwaggerOperation("Lock customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(Guid customerId, LockCustomer command)
    {
        await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
        return NoContent();
    }
        
    [HttpPut("{customerId:guid}/unlock")]
    [Authorize(Policy)]
    [SwaggerOperation("Unlock customer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Post(Guid customerId, UnlockCustomer command)
    {
        await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
        return NoContent();
    }
}