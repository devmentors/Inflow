using System;
using Inflow.Shared.Abstractions.Contracts;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Customers.Core.Events.External;

internal record UserStateUpdated(Guid UserId, string State) : IEvent;
    
[Message("users")]
internal class UserStateUpdatedContract : Contract<UserStateUpdated>
{
    public UserStateUpdatedContract()
    {
        RequireAll();
    }
}