using System;
using System.Collections.Generic;
using System.Linq;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Wallets.Exceptions;
using Inflow.Modules.Wallets.Core.Wallets.Types;
using Inflow.Modules.Wallets.Core.Wallets.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.Types;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Wallets.Core.Wallets.Entities;

internal class Wallet : AggregateRoot<WalletId>
{
    private HashSet<Transfer> _transfers = new();

    public OwnerId OwnerId { get; private set; }
    public Currency Currency { get; private set; }

    public IEnumerable<Transfer> Transfers
    {
        get => _transfers;
        set => _transfers = new HashSet<Transfer>(value);
    }

    public DateTime CreatedAt { get; private set; }

    private Wallet()
    {
    }

    public Wallet(WalletId id, OwnerId ownerId, Currency currency, DateTime createdAt)
    {
        Id = id;
        OwnerId = ownerId;
        Currency = currency;
        CreatedAt = createdAt;
    }
        
    public IReadOnlyCollection<Transfer> TransferFunds(Wallet receiver, Amount amount, DateTime createdAt)
    {
        var outTransferId = new TransferId();
        var inTransferId = new TransferId();

        var outTransfer = DeductFunds(outTransferId, amount, createdAt,
            metadata: GetMetadata(outTransferId, receiver.Id));
            
        var inTransfer = receiver.AddFunds(inTransferId, amount, createdAt,
            metadata: GetMetadata(inTransferId, Id));

        return new List<Transfer> { outTransfer, inTransfer };
            
        static TransferMetadata GetMetadata(TransferId referenceId, WalletId walletId)
            => new($"{{\"referenceId\": \"{referenceId}\", \"walletId\": \"{walletId}\"}}");
    }

    public IncomingTransfer AddFunds(TransferId transferId, Amount amount, DateTime createdAt,
        TransferName name = null, TransferMetadata metadata = null)
    {
        if (amount <= 0)
        {
            throw new InvalidTransferAmountException(amount);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "add_funds";
        }

        var transfer = new IncomingTransfer(transferId, Id, Currency, amount, createdAt, name, metadata);
        _transfers.Add(transfer);
        IncrementVersion();

        return transfer;
    }

    public OutgoingTransfer DeductFunds(TransferId transferId, Amount amount, DateTime createdAt,
        TransferName name = null, TransferMetadata metadata = null)
    {
        if (amount <= 0)
        {
            throw new InvalidTransferAmountException(amount);
        }

        if (CurrentAmount() < amount)
        {
            throw new InsufficientWalletFundsException(Id);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "deduct_funds";
        }

        var transfer = new OutgoingTransfer(transferId, Id, Currency, amount, createdAt, name, metadata);
        _transfers.Add(transfer);
        IncrementVersion();

        return transfer;
    }

    public Amount CurrentAmount()
        => _transfers.OfType<IncomingTransfer>().Sum(x => x.Amount) - _transfers.OfType<OutgoingTransfer>().Sum(x => x.Amount);
}