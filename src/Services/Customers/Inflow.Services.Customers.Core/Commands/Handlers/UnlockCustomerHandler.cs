using System.Threading;
using System.Threading.Tasks;
using Inflow.Services.Customers.Core.Domain.Repositories;
using Inflow.Services.Customers.Core.Events;
using Inflow.Services.Customers.Core.Exceptions;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Inflow.Services.Customers.Core.Commands.Handlers
{
    public sealed class UnlockCustomerHandler : ICommandHandler<UnlockCustomer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<UnlockCustomerHandler> _logger;

        public UnlockCustomerHandler(ICustomerRepository customerRepository, IMessageBroker messageBroker,
            ILogger<UnlockCustomerHandler> logger)
        {
            _customerRepository = customerRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }
        
        public async Task HandleAsync(UnlockCustomer command, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId);
            if (customer is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }
            
            customer.Unlock(command.Notes);
            await _customerRepository.UpdateAsync(customer);
            await _messageBroker.PublishAsync(new CustomerUnlocked(command.CustomerId), cancellationToken);
            _logger.LogInformation($"Unlocked a customer with ID: '{command.CustomerId}'.");
        }
    }
}