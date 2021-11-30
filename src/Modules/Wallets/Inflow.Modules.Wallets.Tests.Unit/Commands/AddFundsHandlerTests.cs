using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.Commands;
using Inflow.Modules.Wallets.Application.Wallets.Commands.Handlers;
using Inflow.Modules.Wallets.Application.Wallets.Events;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.Unit.Commands;

public class AddFundsHandlerTests
{
    private Task Act(AddFunds command) => _handler.HandleAsync(command);
        
    [Fact]
    public async Task given_valid_command_add_funds_should_succeed()
    {
        const decimal funds = 1000;
        const string currency = "EUR";
        var now = _clock.CurrentDate();

        var owner = new IndividualOwner(Guid.NewGuid(), "Owner 1", "John Doe 1", now);
        _ownerRepository.GetAsync(owner.Id).Returns(owner);

        var wallet = new Wallet(Guid.NewGuid(), owner.Id, currency, now);
        _walletRepository.GetAsync(wallet.Id).Returns(wallet);

        var command = new AddFunds(wallet.Id, wallet.Currency, funds);
        await Act(command);

        wallet.CurrentAmount().ShouldBe(new Amount(funds));
        wallet.Transfers.ShouldHaveSingleItem();
        var transfer = wallet.Transfers.Single();
        transfer.ShouldBeOfType<IncomingTransfer>();

        _clock.Received().CurrentDate();
        await _walletRepository.Received().GetAsync(command.WalletId);
        await _walletRepository.Received().UpdateAsync(wallet);
        await _messageBroker.Received()
            .PublishAsync(Arg.Is<FundsAdded>(x => x.WalletId.Equals(wallet.Id) && x.Amount.Equals(funds)
                                                                               && x.Currency.Equals(wallet.Currency)), Arg.Any<CancellationToken>());
    }

    private readonly IIndividualOwnerRepository _ownerRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly ILogger<AddFundsHandler> _logger;
    private readonly IMessageBroker _messageBroker;
    private readonly ICommandHandler<AddFunds> _handler;

    public AddFundsHandlerTests()
    {
        _ownerRepository = Substitute.For<IIndividualOwnerRepository>();
        _walletRepository = Substitute.For<IWalletRepository>();
        _logger = Substitute.For<ILogger<AddFundsHandler>>();
        _clock = Substitute.For<IClock>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _handler = new AddFundsHandler(_walletRepository, _clock, _messageBroker, _logger);
    }
}