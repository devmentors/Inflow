using System;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Inflow.Modules.Customers.Core.Exceptions;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;

namespace Inflow.Modules.Customers.Core.Events.External.Handlers;

internal sealed class SignedUpHandler : IEventHandler<SignedUp>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IClock _clock;
    private readonly ILogger<SignedUpHandler> _logger;

    public SignedUpHandler(ICustomerRepository customerRepository, IClock clock,
        ILogger<SignedUpHandler> logger)
    {
        _customerRepository = customerRepository;
        _clock = clock;
        _logger = logger;
    }
    
    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        if (@event.Role is not "user")
        {
            return;
        }

        var customerId = @event.UserId;
        if (await _customerRepository.GetAsync(customerId) is not null)
        {
            throw new CustomerAlreadyExistsException(customerId);
        }

        var customer = new Customer(customerId, @event.Email, _clock.CurrentDate());
        await _customerRepository.AddAsync(customer);
        _logger.LogInformation($"Created a customer with ID: '{customer.Id}'.");
    }
}