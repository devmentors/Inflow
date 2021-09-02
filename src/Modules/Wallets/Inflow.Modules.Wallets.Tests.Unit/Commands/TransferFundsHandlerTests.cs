using System;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.Commands;
using Inflow.Modules.Wallets.Application.Wallets.Commands.Handlers;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.Unit.Commands
{
    public class TransferFundsHandlerTests
    {
        private Task Act(TransferFunds command) => _handler.HandleAsync(command);
        
        [Fact]
        public async Task given_valid_command_transfer_funds_should_succeed()
        {
            const decimal initialFunds = 1000;
            const decimal sentFunds = 100;
            const string currency = "EUR";
            var now = _clock.CurrentDate();

            var ownerWallet = new Wallet(Guid.NewGuid(), Guid.NewGuid(), currency, now);
            var receiverWallet = new Wallet(Guid.NewGuid(), Guid.NewGuid(), currency, now);
            ownerWallet.AddFunds(Guid.NewGuid(), initialFunds, now, "test_add");

            _walletRepository.GetAsync(ownerWallet.Id).Returns(ownerWallet);
            _walletRepository.GetAsync(receiverWallet.Id).Returns(receiverWallet);

            var command = new TransferFunds(ownerWallet.OwnerId, ownerWallet.Id, receiverWallet.Id,
                currency, sentFunds);
            await Act(command);

            _clock.Received().CurrentDate();
            await _walletRepository.Received().GetAsync(ownerWallet.Id);
            await _walletRepository.Received().GetAsync(receiverWallet.Id);
            await _walletRepository.Received().UpdateAsync(ownerWallet);
            await _walletRepository.Received().UpdateAsync(receiverWallet);
            await _messageBroker.Received().PublishAsync(Arg.Any<IMessage[]>(),
                Arg.Any<CancellationToken>());
        }

        private readonly IIndividualOwnerRepository _ownerRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IClock _clock;
        private readonly ILogger<TransferFundsHandler> _logger;
        private readonly IMessageBroker _messageBroker;
        private readonly ICommandHandler<TransferFunds> _handler;

        public TransferFundsHandlerTests()
        {
            _ownerRepository = Substitute.For<IIndividualOwnerRepository>();
            _walletRepository = Substitute.For<IWalletRepository>();
            _logger = Substitute.For<ILogger<TransferFundsHandler>>();
            _clock = Substitute.For<IClock>();
            _messageBroker = Substitute.For<IMessageBroker>();
            _handler = new TransferFundsHandler(_walletRepository, _clock, _messageBroker, _logger);
        }
    }
}