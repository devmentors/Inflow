using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Wallets.Application.Wallets.Events
{
    internal record WalletAdded(Guid WalletId, Guid OwnerId, string Currency) : IEvent;
}