using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Repositories;
using Inflow.Modules.Payments.Core.Withdrawals.Events;
using Inflow.Modules.Payments.Core.Withdrawals.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Exceptions;
using Inflow.Modules.Payments.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Payments.Core.Withdrawals.Commands.Handlers
{
    internal sealed class StartWithdrawalHandler : ICommandHandler<StartWithdrawal>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWithdrawalRepository _withdrawalRepository;
        private readonly IWithdrawalAccountRepository _withdrawalAccountRepository;
        private readonly IClock _clock;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<StartWithdrawalHandler> _logger;

        public StartWithdrawalHandler(ICustomerRepository customerRepository, IWithdrawalRepository withdrawalRepository,
            IWithdrawalAccountRepository withdrawalAccountRepository, IClock clock, IMessageBroker messageBroker,
            ILogger<StartWithdrawalHandler> logger)
        {
            _customerRepository = customerRepository;
            _withdrawalRepository = withdrawalRepository;
            _withdrawalAccountRepository = withdrawalAccountRepository;
            _clock = clock;
            _messageBroker = messageBroker;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartWithdrawal command, CancellationToken cancellationToken = default)
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
            
            var account = await _withdrawalAccountRepository.GetAsync(command.CustomerId, command.Currency);
            if (account is null)
            {
                throw new WithdrawalAccountNotFoundException(command.AccountId, command.CustomerId);
            }
            
            var withdrawal = account.CreateWithdrawal(command.WithdrawalId, command.Amount, _clock.CurrentDate());
            await _withdrawalRepository.AddAsync(withdrawal);
            await _messageBroker.PublishAsync(new WithdrawalStarted(command.WithdrawalId, command.CustomerId,
                command.Currency, command.Amount), cancellationToken);
            _logger.LogInformation($"Started a withdrawal with ID: '{command.WithdrawalId}'.");
        }
    }
}