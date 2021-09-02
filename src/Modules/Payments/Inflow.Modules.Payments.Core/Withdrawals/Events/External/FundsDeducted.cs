using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Withdrawals.Events.External
{
    internal record FundsDeducted(Guid WalletId, Guid OwnerId, string Currency, decimal Amount, string TransferName = null,
        string TransferMetadata = null) : IEvent;
}