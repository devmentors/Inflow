using System;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Modules.Wallets.Core.Wallets.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Wallets.Core.Wallets.Entities
{
    internal abstract class Transfer
    {
        public TransferId Id { get; private set; }
        public WalletId WalletId { get; private set; }
        public Currency Currency { get; private set; }
        public Amount Amount { get; private set; }
        public TransferName Name { get; private set; }
        public TransferMetadata Metadata { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Transfer()
        {
        }

        protected Transfer(TransferId id, WalletId walletId, Currency currency, Amount amount, DateTime createdAt,
            TransferName name = null, TransferMetadata metadata = null)
        {
            Id = id;
            WalletId = walletId;
            Currency = currency;
            Amount = amount;
            CreatedAt = createdAt;
            Name = name;
            Metadata = metadata;
        }
    }
}