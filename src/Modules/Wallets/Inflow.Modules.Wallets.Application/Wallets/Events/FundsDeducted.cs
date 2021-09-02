using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Wallets.Application.Wallets.Events
{
    internal record FundsDeducted(Guid WalletId, Guid OwnerId, string Currency, decimal Amount, string TransferName = null,
        string TransferMetadata = null) : IEvent;
}