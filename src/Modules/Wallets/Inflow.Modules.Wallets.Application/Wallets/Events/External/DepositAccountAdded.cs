using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Wallets.Application.Wallets.Events.External
{
    internal record DepositAccountAdded(Guid AccountId, Guid CustomerId, string Currency) : IEvent;
}