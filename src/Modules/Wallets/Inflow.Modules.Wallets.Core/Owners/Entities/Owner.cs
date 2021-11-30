using System;
using Inflow.Modules.Wallets.Core.Owners.Types;
using Inflow.Modules.Wallets.Core.Owners.ValueObjects;

namespace Inflow.Modules.Wallets.Core.Owners.Entities;

internal abstract class Owner
{
    public OwnerId Id { get; private set; }
    public OwnerName Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? VerifiedAt { get; private set; }

    protected Owner()
    {
    }

    public Owner(OwnerId id, OwnerName name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsActive = true;
        CreatedAt = createdAt;
    }

    public void Verify(DateTime verifiedAt)
    {
        VerifiedAt = verifiedAt;
    }

    public bool Lock() => IsActive = false;
        
    public bool Unlock() => IsActive = true;
}