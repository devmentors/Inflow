using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Domain.Repositories;
using Inflow.Modules.Payments.Core.Withdrawals.Exceptions;
using Inflow.Modules.Payments.Core.Withdrawals.Services;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Payments.Core.Withdrawals.Events.External.Handlers;

internal sealed class FundsDeductedHandler : IEventHandler<FundsDeducted>
{
    private const string TransferName = "withdrawal";
    private readonly IWithdrawalRepository _withdrawalRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IWithdrawalMetadataResolver _metadataResolver;
    private readonly IClock _clock;
    private readonly ILogger<FundsDeductedHandler> _logger;

    public FundsDeductedHandler(IWithdrawalRepository withdrawalRepository, IMessageBroker messageBroker,
        IWithdrawalMetadataResolver metadataResolver, IClock clock, ILogger<FundsDeductedHandler> logger)
    {
        _withdrawalRepository = withdrawalRepository;
        _messageBroker = messageBroker;
        _metadataResolver = metadataResolver;
        _clock = clock;
        _logger = logger;
    }
        
    public async Task HandleAsync(FundsDeducted @event, CancellationToken cancellationToken = default)
    {
        if (@event.TransferName != TransferName)
        {
            return;
        }

        var withdrawalId = _metadataResolver.TryResolveWithdrawalId(@event.TransferMetadata);
        if (!withdrawalId.HasValue)
        {
            return;
        }
            
        var withdrawal = await _withdrawalRepository.GetAsync(withdrawalId.Value);
        if (withdrawal is null)
        {
            throw new WithdrawalNotFoundException(withdrawalId.Value);
        }
            
        withdrawal.Complete(_clock.CurrentDate());
        await _withdrawalRepository.UpdateAsync(withdrawal);
        await _messageBroker.PublishAsync(new WithdrawalCompleted(withdrawal.Id, withdrawal.Account.CustomerId,
            withdrawal.Currency, withdrawal.Amount), cancellationToken);
        _logger.LogInformation($"Completed withdrawal with ID: '{withdrawal.Id}'.");
    }
}