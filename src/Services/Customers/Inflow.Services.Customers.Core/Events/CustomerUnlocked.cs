using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Services.Customers.Core.Events
{
    public record CustomerUnlocked(Guid CustomerId) : IEvent;
}