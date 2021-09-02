using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages
{
    internal record DepositCompleted(Guid DepositId, Guid CustomerId, string Currency, decimal Amount) : IEvent;
}