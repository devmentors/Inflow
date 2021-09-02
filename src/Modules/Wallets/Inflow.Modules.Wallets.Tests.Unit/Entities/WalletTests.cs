using System;
using System.Linq;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Modules.Wallets.Core.Wallets.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Shouldly;
using Xunit;

namespace Inflow.Modules.Wallets.Tests.Unit.Entities
{
    public class WalletTests
    {
        [Fact]
        public void given_valid_amount_add_funds_should_succeed_and_return_incoming_transfer()
        {
            var amount = new Amount(1000);
            var transferId = new TransferId(Guid.NewGuid());
            var transferName = new TransferName("test name");
            var transferMetadata = new TransferMetadata("test metadata");
            var now = DateTime.UtcNow;
            var wallet = CreateWallet();

            var transfer = wallet.AddFunds(transferId, amount, now, transferName, transferMetadata);

            wallet.Transfers.ShouldHaveSingleItem();
            wallet.Transfers.Single().ShouldBe(transfer);
            wallet.Version.ShouldBe(2);
            transfer.Id.ShouldBe(transferId);
            transfer.WalletId.ShouldBe(wallet.Id);
            transfer.Amount.ShouldBe(amount);
            transfer.Currency.ShouldBe(wallet.Currency);
            transfer.Name.ShouldBe(transferName);
            transfer.Metadata.ShouldBe(transferMetadata);
            transfer.Currency.ShouldBe(wallet.Currency);
            transfer.CreatedAt.ShouldBe(now);
        }

        [Fact]
        public void given_incoming_and_outgoing_transfers_wallet_amount_should_be_properly_calculated()
        {
            var incomingAmount = new Amount(1000);
            var outgoingAmount = new Amount(200);
            var expectedAmount = incomingAmount - outgoingAmount;

            var now = DateTime.UtcNow;
            var wallet = CreateWallet();

            wallet.AddFunds(new TransferId(Guid.NewGuid()), incomingAmount, now);
            wallet.DeductFunds(new TransferId(Guid.NewGuid()), outgoingAmount, now);

            wallet.CurrentAmount().ShouldBe(expectedAmount);

            wallet.Transfers.Count().ShouldBe(2);
            wallet.Version.ShouldBe(2);
        }

        private static Wallet CreateWallet() => new(Guid.NewGuid(), Guid.NewGuid(), "EUR", DateTime.UtcNow);
    }
}