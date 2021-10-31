using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Saga.Api.Messages
{
    [ExternalMessage("customers", queue: "saga/customers-service.customer_completed")]
    internal record CustomerCompleted(Guid CustomerId, string Name, string FullName, string Nationality) : IEvent;
}