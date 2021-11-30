using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions;

internal class InsufficientWalletFundsException : InflowException
{
    public Guid WalletId { get; }

    public InsufficientWalletFundsException(Guid walletId)
        : base($"Insufficient funds for wallet with ID: '{walletId}'.")
    {
        WalletId = walletId;
    }
}