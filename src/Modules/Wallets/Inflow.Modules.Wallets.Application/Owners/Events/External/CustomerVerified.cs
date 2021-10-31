using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Wallets.Application.Owners.Events.External
{
    [ExternalMessage("customers", queue: "wallets-module/customers-service.customer_verified")]
    internal record CustomerVerified(Guid CustomerId) : IEvent;
}