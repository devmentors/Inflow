using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.Events;
using Inflow.Modules.Wallets.Core.Wallets.Exceptions;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;

namespace Inflow.Modules.Wallets.Application.Wallets.Commands.Handlers
{
    internal sealed class AddFundsHandler : ICommandHandler<AddFunds>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IClock _clock;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<AddFundsHandler> _logger;

        public AddFundsHandler(IWalletRepository walletRepository, IClock clock, IMessageBroker messageBroker,
            ILogger<AddFundsHandler> logger)
        {
            _walletRepository = walletRepository;
            _clock = clock;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(AddFunds command, CancellationToken cancellationToken = default)
        {
            var wallet = await _walletRepository.GetAsync(command.WalletId);
            if (wallet is null)
            {
                throw new WalletNotFoundException(command.WalletId);
            }

            if (wallet.Currency != command.Currency)
            {
                throw new InvalidTransferCurrencyException(command.Currency);
            }

            var transfer = wallet.AddFunds(command.TransferId, command.Amount, _clock.CurrentDate(),
                command.TransferName, command.TransferMetadata);
            await _walletRepository.UpdateAsync(wallet);
            await _messageBroker.PublishAsync(new FundsAdded(wallet.Id, wallet.OwnerId, wallet.Currency,
                transfer.Amount, transfer.Name, transfer.Metadata), cancellationToken);
            _logger.LogInformation($"Added {transfer.Amount} {transfer.Currency} to wallet with ID: '{wallet.Id}'.");
        }
    }
}