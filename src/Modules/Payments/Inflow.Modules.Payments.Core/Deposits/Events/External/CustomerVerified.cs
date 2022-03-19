using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External;

internal record CustomerVerified(Guid CustomerId) : IEvent;