using System;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Owners.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Wallets.Core.Owners.Entities
{
    internal class IndividualOwner : Owner
    {
        public FullName FullName { get; private set; }

        private IndividualOwner()
        {
        }
        
        public IndividualOwner(OwnerId id, OwnerName name, FullName fullName, DateTime createdAt) : base(id, name, createdAt)
        {
            FullName = fullName;
        }
    }
}