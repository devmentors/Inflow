using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Services.Customers.Core.Events.External
{
    [ExternalMessage("inflow")]
    public record UserStateUpdated(Guid UserId, string State) : IEvent;
}