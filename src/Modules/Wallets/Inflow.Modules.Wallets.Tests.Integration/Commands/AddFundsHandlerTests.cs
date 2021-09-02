using System;
using System.Linq;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.Commands;
using Inflow.Modules.Wallets.Application.Wallets.Commands.Handlers;
using Inflow.Modules.Wallets.Application.Wallets.Events;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Repositories;
using Inflow.Modules.Wallets.Infrastructure.EF.Repositories;
using Inflow.Modules.Wallets.Tests.Integration.Common;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure.Time;
using Inflow.Shared.Tests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.Integration.Commands
{
    public class AddFundsHandlerTests : IDisposable
    {
        private Task Act(AddFunds command) => _handler.HandleAsync(command);
        
        [Fact]
        public async Task given_valid_command_add_funds_should_succeed()
        {
            await _dbContext.Context.Database.EnsureCreatedAsync();
            
            const decimal funds = 1000;
            const string currency = "EUR";
            var now = _clock.CurrentDate();
            var owner = new IndividualOwner(Guid.NewGuid(), "Owner 1", "John Doe 1", now);
            await _ownerRepository.AddAsync(owner);
            
            var wallet = new Wallet(Guid.NewGuid(), owner.Id, currency, now);
            await _walletRepository.AddAsync(wallet);

            var command = new AddFunds(wallet.Id, wallet.Currency, funds);
            await Act(command);

            var updatedWallet = await _walletRepository.GetAsync(wallet.Id);
            updatedWallet.CurrentAmount().ShouldBe(new Amount(funds));
            updatedWallet.Transfers.ShouldHaveSingleItem();
            var transfer = updatedWallet.Transfers.Single();
            transfer.ShouldBeOfType<IncomingTransfer>();
            
            _messageBroker.Messages.ShouldNotBeEmpty();
            _messageBroker.Messages.Count.ShouldBe(1);
            var @event = _messageBroker.Messages[0];
            @event.ShouldBeOfType<FundsAdded>();
        }

        private readonly TestWalletsDbContext _dbContext;
        private readonly IIndividualOwnerRepository _ownerRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IClock _clock;
        private readonly ILogger<AddFundsHandler> _logger;
        private readonly TestMessageBroker _messageBroker;
        private readonly ICommandHandler<AddFunds> _handler;

        public AddFundsHandlerTests()
        {
            _dbContext = new TestWalletsDbContext();
            _ownerRepository = new IndividualOwnerRepository(_dbContext.Context);
            _walletRepository = new WalletRepository(_dbContext.Context);
            _clock = new UtcClock();
            _logger = new NullLogger<AddFundsHandler>();
            _messageBroker = new TestMessageBroker();
            _handler = new AddFundsHandler(_walletRepository, _clock, _messageBroker, _logger);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}