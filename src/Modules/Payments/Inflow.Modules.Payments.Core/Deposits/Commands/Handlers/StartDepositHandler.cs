using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Deposits.Domain.Repositories;
using Inflow.Modules.Payments.Core.Deposits.Events;
using Inflow.Modules.Payments.Core.Deposits.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Payments.Core.Deposits.Commands.Handlers
{
    internal sealed class StartDepositHandler : ICommandHandler<StartDeposit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDepositRepository _depositRepository;
        private readonly IDepositAccountRepository _depositAccountRepository;
        private readonly IClock _clock;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<StartDepositHandler> _logger;

        public StartDepositHandler(ICustomerRepository customerRepository, IDepositRepository depositRepository,
            IDepositAccountRepository depositAccountRepository, IClock clock, IMessageBroker messageBroker,
            ILogger<StartDepositHandler> logger)
        {
            _customerRepository = customerRepository;
            _depositRepository = depositRepository;
            _depositAccountRepository = depositAccountRepository;
            _clock = clock;
            _messageBroker = messageBroker;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDeposit command, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId);
            if (customer is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            if (!customer.IsActive || !customer.IsVerified)
            {
                throw new CustomerNotActiveException(command.CustomerId);
            }
            
            var account = await _depositAccountRepository.GetAsync(command.CustomerId, command.Currency);
            if (account is null)
            {
                throw new DepositAccountNotFoundException(command.AccountId, command.CustomerId);
            }

            var deposit = account.CreateDeposit(command.DepositId, command.Amount, _clock.CurrentDate());
            await _depositRepository.AddAsync(deposit);
            await _messageBroker.PublishAsync(new DepositStarted(command.DepositId, command.CustomerId,
                command.Currency, command.Amount), cancellationToken);
            _logger.LogInformation($"Started a deposit with ID: '{command.DepositId}'.");
        }
    }
}