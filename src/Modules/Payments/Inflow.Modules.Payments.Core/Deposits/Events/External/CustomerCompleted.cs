using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Payments.Core.Deposits.Events.External;

internal record CustomerCompleted(Guid CustomerId, string FullName, string Nationality) : IEvent;