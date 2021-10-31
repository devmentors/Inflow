using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Services.Customers.Core.Events
{
    public record CustomerCompleted(Guid CustomerId, string Name, string FullName, string Nationality) : IEvent;
}