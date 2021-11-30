using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Infrastructure.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External.Handlers;

internal sealed class CustomerUnlockedHandler : IEventHandler<CustomerUnlocked>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerUnlockedHandler> _logger;

    public CustomerUnlockedHandler(ICustomerRepository customerRepository,
        ILogger<CustomerUnlockedHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }
        
    public async Task HandleAsync(CustomerUnlocked @event, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(@event.CustomerId);
        if (customer is null)
        {
            throw new CustomerNotFoundException(@event.CustomerId);
        }
            
        customer.Unlock();
        await _customerRepository.UpdateAsync(customer);
    }
}