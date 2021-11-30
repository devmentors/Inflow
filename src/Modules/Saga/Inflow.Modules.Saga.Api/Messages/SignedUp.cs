using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Messages;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;