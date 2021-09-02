using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Customers.Core.Events
{
    internal record CustomerCompleted(Guid CustomerId, string Name, string FullName, string Nationality) : IEvent;
}