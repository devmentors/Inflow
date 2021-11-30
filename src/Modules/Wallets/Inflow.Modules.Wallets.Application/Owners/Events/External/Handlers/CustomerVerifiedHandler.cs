using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Owners.Exceptions;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Wallets.Application.Owners.Events.External.Handlers;

internal sealed class CustomerVerifiedHandler : IEventHandler<CustomerVerified>
{
    private readonly IIndividualOwnerRepository _ownerRepository;
    private readonly IClock _clock;
    private readonly ILogger<CustomerVerifiedHandler> _logger;

    public CustomerVerifiedHandler(IIndividualOwnerRepository ownerRepository, IClock clock,
        ILogger<CustomerVerifiedHandler> logger)
    {
        _ownerRepository = ownerRepository;
        _clock = clock;
        _logger = logger;
    }

    public async Task HandleAsync(CustomerVerified @event, CancellationToken cancellationToken = default)
    {
        var owner = await _ownerRepository.GetAsync(@event.CustomerId);
        if (owner is null)
        {
            throw new OwnerNotFoundException(@event.CustomerId);
        }
            
        owner.Verify(_clock.CurrentDate());
        await _ownerRepository.UpdateAsync(owner);
        _logger.LogInformation($"Verified individual owner with ID: '{owner.Id}' based on customer.");
    }
}