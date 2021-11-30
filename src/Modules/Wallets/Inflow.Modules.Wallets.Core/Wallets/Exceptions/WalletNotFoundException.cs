using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions;

public class WalletNotFoundException : InflowException
{
    public Guid OwnerId { get; }
    public string Currency { get; }
    public Guid WalletId { get; }

    public WalletNotFoundException(Guid walletId) : base($"Wallet with ID: '{walletId}' was not found.")
    {
        WalletId = walletId;
    }
        
    public WalletNotFoundException(Guid ownerId, string currency)
        : base($"Wallet for owner with ID: '{ownerId}', currency: '{currency}' was not found.")
    {
        OwnerId = ownerId;
        Currency = currency;
    }
}