using System.Threading;
using System.Threading.Tasks;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Customers.Core.Events.External.Handlers;

internal sealed class SignedUpHandler : IEventHandler<SignedUp>
{
    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}