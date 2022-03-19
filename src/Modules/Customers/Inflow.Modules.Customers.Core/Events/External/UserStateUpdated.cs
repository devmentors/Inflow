using System;
using Inflow.Shared.Abstractions.Contracts;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Customers.Core.Events.External;

[Message("users")]
internal record UserStateUpdated(Guid UserId, string State) : IEvent;

internal class UserStateUpdatedContract : Contract<UserStateUpdated>
{
    public UserStateUpdatedContract()
    {
        RequireAll();
    }
}
