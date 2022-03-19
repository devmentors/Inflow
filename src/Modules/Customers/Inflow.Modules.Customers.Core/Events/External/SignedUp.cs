using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Customers.Core.Events.External;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;