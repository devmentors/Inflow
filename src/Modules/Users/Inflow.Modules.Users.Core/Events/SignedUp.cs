using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}