using System;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Core.Wallets.Exceptions;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Wallets.Application.Wallets.Events.External.Handlers;

internal class DepositCompletedHandler : IEventHandler<DepositCompleted>
{
    private const string TransferName = "deposit";
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<DepositCompletedHandler> _logger;

    public DepositCompletedHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
        ILogger<DepositCompletedHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(DepositCompleted @event, CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetAsync(@event.CustomerId, @event.Currency);
        if (wallet is null)
        {
            throw new WalletNotFoundException(@event.CustomerId, @event.Currency);
        }

        var transfer = wallet.AddFunds(Guid.NewGuid(), @event.Amount, _clock.CurrentDate(),
            TransferName, GetMetadata(@event.DepositId));
        await _walletRepository.UpdateAsync(wallet);
        await _messageBroker.PublishAsync(new FundsAdded(wallet.Id, wallet.OwnerId, wallet.Currency,
            @event.Amount, transfer.Name, transfer.Metadata), cancellationToken);
        _logger.LogInformation($"Added {@event.Amount} {wallet.Currency} to wallet with ID: '{wallet.Id}'" +
                               $"based on completed deposit with ID: '{@event.DepositId}'.");
    }

    private static string GetMetadata(Guid depositId) => $"{{\"depositId\": \"{depositId}\"}}";
}