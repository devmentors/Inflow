using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions
{
    public class WalletAlreadyExistsException : InflowException
    {
        public Guid OwnerId { get; }
        public string Currency { get; }
        
        public WalletAlreadyExistsException(Guid ownerId, string currency)
            : base($"Wallet for owner with ID: '{ownerId}', currency: '{currency}' already exists.")
        {
            OwnerId = ownerId;
            Currency = currency;
        }
    }
}