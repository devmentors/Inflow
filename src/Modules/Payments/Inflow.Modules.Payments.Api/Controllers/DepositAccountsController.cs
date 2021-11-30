using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Deposits.DTO;
using Inflow.Modules.Payments.Core.Deposits.Queries;
using Inflow.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Inflow.Modules.Payments.Api.Controllers;

[ApiController]
[Route("deposits/accounts")]
internal class DepositAccountsController : Controller
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public DepositAccountsController(IDispatcher dispatcher, IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Browse deposit accounts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Paged<DepositAccountDto>>> BrowseAsync([FromQuery] BrowseDepositAccounts query)
    {
        if (query.CustomerId.HasValue || _context.Identity.IsUser())
        {
            // Customer cannot access the other deposit accounts
            query.CustomerId = _context.Identity.IsUser() ? _context.Identity.Id : query.CustomerId;
        }
            
        return Ok(await _dispatcher.QueryAsync(query));
    }
}