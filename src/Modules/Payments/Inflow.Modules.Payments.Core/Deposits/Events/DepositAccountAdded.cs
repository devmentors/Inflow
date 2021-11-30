using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events;

internal record DepositAccountAdded(Guid AccountId, Guid CustomerId, string Currency) : IEvent;