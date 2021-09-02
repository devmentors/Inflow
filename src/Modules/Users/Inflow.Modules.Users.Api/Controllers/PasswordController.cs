using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Modules.Users.Core.Commands;
using Inflow.Shared.Abstractions.Contexts;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace Inflow.Modules.Users.Api.Controllers
{
    internal class PasswordController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        private readonly IContext _context;

        public PasswordController(IDispatcher dispatcher, IContext context)
        {
            _dispatcher = dispatcher;
            _context = context;
        }
        
        [Authorize]
        [HttpPut]
        [SwaggerOperation("Change password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeAsync(ChangePassword command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.UserId, _context.Identity.Id));
            return NoContent();
        }
    }
}