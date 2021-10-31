using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Saga.Api.Messages
{
    [ExternalMessage("customers", queue: "saga/customers-service.customer_verified")]
    internal record CustomerVerified(Guid CustomerId) : IEvent;
}