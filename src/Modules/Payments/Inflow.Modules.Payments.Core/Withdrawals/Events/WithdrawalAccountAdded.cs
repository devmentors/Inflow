using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Withdrawals.Events
{
    internal record WithdrawalAccountAdded(Guid AccountId, Guid CustomerId, string Currency) : IEvent;
}