using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages
{
    internal record CustomerVerified(Guid CustomerId) : IEvent;
}