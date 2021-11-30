using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages;

internal record WalletAdded(Guid WalletId, Guid OwnerId, string Currency) : IEvent;