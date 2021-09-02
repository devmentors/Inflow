using System;
using Inflow.Shared.Abstractions.Contracts;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Wallets.Application.Owners.Events.External
{
    internal record CustomerVerified(Guid CustomerId) : IEvent;

    [Message("customers")]
    internal class CustomerVerifiedContract : Contract<CustomerVerified>
    {
        public CustomerVerifiedContract()
        {
            RequireAll();
        }
    }
}