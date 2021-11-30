using System;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Services.Customers.Core.Events.External;

[ExternalMessage("inflow")]
public record SignedUp(Guid UserId, string Email, string Role) : IEvent;