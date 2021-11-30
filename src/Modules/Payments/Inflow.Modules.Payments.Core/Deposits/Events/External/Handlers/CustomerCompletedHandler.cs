using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Infrastructure.Entities;
using Inflow.Modules.Payments.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External.Handlers;

internal sealed class CustomerCompletedHandler : IEventHandler<CustomerCompleted>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerCompletedHandler> _logger;

    public CustomerCompletedHandler(ICustomerRepository customerRepository,
        ILogger<CustomerCompletedHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }
        
    public async Task HandleAsync(CustomerCompleted @event, CancellationToken cancellationToken = default)
    {
        var customer = new Customer(@event.CustomerId, @event.FullName, @event.Nationality);
        await _customerRepository.AddAsync(customer);
    }
}