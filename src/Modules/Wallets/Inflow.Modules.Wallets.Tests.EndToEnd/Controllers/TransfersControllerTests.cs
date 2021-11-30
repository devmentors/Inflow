using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Inflow.Bootstrapper;
using Inflow.Modules.Wallets.Application.Wallets.Commands;
using Inflow.Modules.Wallets.Core.Owners.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Tests.EndToEnd.Common;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure.Time;
using Inflow.Shared.Tests.EndToEnd;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.EndToEnd.Controllers;

public class TransfersControllerTests : WebApiTestBase
{
    [Fact]
    public async Task post_transfers_incoming_given_valid_request_should_succeed()
    {
        await _dbContext.Context.Database.EnsureCreatedAsync();

        const decimal funds = 1000;
        const string currency = "EUR";
        var now = _clock.CurrentDate();
        var owner = new IndividualOwner(Guid.NewGuid(), "Owner 1", "John Doe 1", now);
        await _dbContext.Context.IndividualOwners.AddAsync(owner);

        var wallet = new Wallet(Guid.NewGuid(), owner.Id, currency, now);
        await _dbContext.Context.Wallets.AddAsync(wallet);
        await _dbContext.Context.SaveChangesAsync();

        Authenticate(owner.Id, new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = new[] { "transfers" }
        });
            
        var command = new AddFunds(wallet.Id, wallet.Currency, funds);
        var response = await PostAsync("incoming", command);
            
        response.IsSuccessStatusCode.ShouldBeTrue();
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
        
    [Fact]
    public async Task post_transfers_funds_given_valid_request_should_succeed()
    {
        await _dbContext.Context.Database.EnsureCreatedAsync();

        const decimal initialFunds = 1000;
        const decimal sentFunds = 100;
        const string currency = "EUR";
        var now = _clock.CurrentDate();
            
        var owner = new IndividualOwner(Guid.NewGuid(), "Owner 1", "John Doe 1", now);
        var receiver = new IndividualOwner(Guid.NewGuid(), "Owner 2", "John Doe 2", now);
        await _dbContext.Context.IndividualOwners.AddAsync(owner);
        await _dbContext.Context.IndividualOwners.AddAsync(receiver);
            
        var ownerWallet = new Wallet(Guid.NewGuid(), owner.Id, currency, now);
        var receiverWallet = new Wallet(Guid.NewGuid(), receiver.Id, currency, now);
        await _dbContext.Context.Wallets.AddAsync(ownerWallet);
        await _dbContext.Context.Wallets.AddAsync(receiverWallet);
            
        ownerWallet.AddFunds(Guid.NewGuid(), initialFunds, now, "test_add");

        await _dbContext.Context.SaveChangesAsync();

        Authenticate(owner.Id);
            
        var command = new TransferFunds(owner.Id, ownerWallet.Id, receiverWallet.Id, currency, sentFunds);
        var response = await PostAsync("funds", command);
            
        response.IsSuccessStatusCode.ShouldBeTrue();
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    private readonly TestWalletsDbContext _dbContext;
    private readonly IClock _clock;
        
    public TransfersControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
    {
        _dbContext = new TestWalletsDbContext();
        _clock = new UtcClock();
        SetPath("transfers");
    }

    public override void Dispose()
    {
        _dbContext.Dispose();
        base.Dispose();
    }
}