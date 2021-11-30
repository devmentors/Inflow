using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Infrastructure.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External.Handlers;

internal sealed class CustomerLockedHandler : IEventHandler<CustomerLocked>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerLockedHandler> _logger;

    public CustomerLockedHandler(ICustomerRepository customerRepository,
        ILogger<CustomerLockedHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }
        
    public async Task HandleAsync(CustomerLocked @event, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(@event.CustomerId);
        if (customer is null)
        {
            throw new CustomerNotFoundException(@event.CustomerId);
        }
            
        customer.Lock();
        await _customerRepository.UpdateAsync(customer);
    }
}