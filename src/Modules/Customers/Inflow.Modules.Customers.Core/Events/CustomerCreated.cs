using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Customers.Core.Events;

internal record CustomerCreated(Guid CustomerId) : IEvent;