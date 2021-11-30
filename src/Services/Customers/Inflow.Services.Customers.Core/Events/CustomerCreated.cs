using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Services.Customers.Core.Events;

public record CustomerCreated(Guid CustomerId) : IEvent;