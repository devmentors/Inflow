using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Commands;
using Inflow.Modules.Payments.Core.Withdrawals.DTO;
using Inflow.Modules.Payments.Core.Withdrawals.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Contexts;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace Inflow.Modules.Payments.Api.Controllers
{
    [ApiController]
    [Route("withdrawals/accounts")]
    internal class WithdrawalAccountsController : Controller
    {
        private readonly IDispatcher _dispatcher;
        private readonly IContext _context;

        public WithdrawalAccountsController(IDispatcher dispatcher, IContext context)
        {
            _dispatcher = dispatcher;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Browse withdrawal accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Paged<WithdrawalAccountDto>>> BrowseAsync(
            [FromQuery] BrowseWithdrawalAccounts query)
        {
            if (query.CustomerId.HasValue || _context.Identity.IsUser())
            {
                // Customer cannot access the other withdrawal accounts
                query.CustomerId = _context.Identity.IsUser() ? _context.Identity.Id : query.CustomerId;
            }
            
            return Ok(await _dispatcher.QueryAsync(query));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Add withdrawal account")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Post(AddWithdrawalAccount command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, _context.Identity.Id));
            return NoContent();
        }
    }
}