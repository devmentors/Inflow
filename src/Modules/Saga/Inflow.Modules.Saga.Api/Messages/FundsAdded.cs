using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages;

internal record FundsAdded(Guid WalletId, Guid OwnerId, string Currency, decimal Amount, string TransferName = null,
    string TransferMetadata = null) : IEvent;