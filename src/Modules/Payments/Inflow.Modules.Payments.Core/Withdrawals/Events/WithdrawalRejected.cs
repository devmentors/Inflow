using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Withdrawals.Events;

internal record WithdrawalRejected(Guid WithdrawalId, Guid CustomerId, string Currency, decimal Amount) : IEvent;