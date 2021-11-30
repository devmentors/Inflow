using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Inflow.Modules.Customers.Core.Events.External.Handlers;

internal sealed class UserStateUpdatedHandler : IEventHandler<UserStateUpdated>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<UserStateUpdatedHandler> _logger;

    public UserStateUpdatedHandler(ICustomerRepository customerRepository, IMessageBroker messageBroker, 
        ILogger<UserStateUpdatedHandler> logger)
    {
        _customerRepository = customerRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(UserStateUpdated @event, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(@event.UserId);
        if (customer is null)
        {
            return;
        }

        IEvent integrationEvent;
        switch (@event.State.ToLowerInvariant())
        {
            case "active":
                customer.Unlock();
                integrationEvent = new CustomerUnlocked(customer.Id);
                break;
            case "locked":
                customer.Lock();
                integrationEvent = new CustomerLocked(customer.Id);
                break;
            default:
                _logger.LogWarning($"Received an unknown user state: '{@event.State}'.");
                return;
        }

        await _customerRepository.UpdateAsync(customer);
        await _messageBroker.PublishAsync(integrationEvent, cancellationToken);
        _logger.LogInformation($"{(customer.IsActive ? "Unlocked" : "Locked")} " +
                               $"customer with ID: '{customer.Id}'.");
    }
}