using System;
using Inflow.Shared.Abstractions.Contracts;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Modules.Wallets.Application.Owners.Events.External
{
    internal record CustomerCompleted(Guid CustomerId, string Name, string FullName, string Nationality) : IEvent;

    [Message("customers")]
    internal class CustomerCompletedContract : Contract<CustomerCompleted>
    {
        public CustomerCompletedContract()
        {
            RequireAll();
        }
    }
}