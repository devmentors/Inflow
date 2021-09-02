using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages
{
    internal record CustomerCompleted(Guid CustomerId, string Name, string FullName, string Nationality) : IEvent;
}