using System;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Owners.Events.External;
using Inflow.Modules.Wallets.Application.Owners.Events.External.Handlers;
using Inflow.Modules.Wallets.Core.Owners.Repositories;
using Inflow.Modules.Wallets.Infrastructure.EF;
using Inflow.Modules.Wallets.Infrastructure.EF.Repositories;
using Inflow.Modules.Wallets.Tests.Integration.Common;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure.Time;
using Inflow.Shared.Tests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.Integration.Events
{
    public class CustomerCompletedHandlerTests : IDisposable
    {
        [Fact]
        public async Task given_valid_customer_data_individual_owner_should_be_added()
        {
            await _dbContext.Context.Database.EnsureCreatedAsync();
            var customerId = Guid.NewGuid();
            var @event = new CustomerCompleted(customerId, $"owner-{customerId}", "John Doe", "PL");
            await _handler.HandleAsync(@event);
            var owner = await _individualOwnerRepository.GetAsync(customerId);
            owner.ShouldNotBeNull();
        }

        private readonly TestDbContext<WalletsDbContext> _dbContext;
        private readonly IIndividualOwnerRepository _individualOwnerRepository;
        private readonly IClock _clock;
        private readonly ILogger<CustomerCompletedHandler> _logger;
        private readonly IEventHandler<CustomerCompleted> _handler;

        public CustomerCompletedHandlerTests()
        {
            _dbContext = new TestWalletsDbContext();
            _individualOwnerRepository = new IndividualOwnerRepository(_dbContext.Context);
            _clock = new UtcClock();
            _logger = new NullLogger<CustomerCompletedHandler>();
            _handler = new CustomerCompletedHandler(_individualOwnerRepository, _clock, _logger);
        }


        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}