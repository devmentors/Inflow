using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External
{
    [ExternalMessage("customers", queue: "payments-module/customers-service.customer_locked")]
    internal record CustomerLocked(Guid CustomerId) : IEvent;
}