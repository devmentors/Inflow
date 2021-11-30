using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events;

internal record DepositRejected(Guid DepositId, Guid CustomerId, string Currency, decimal Amount) : IEvent;