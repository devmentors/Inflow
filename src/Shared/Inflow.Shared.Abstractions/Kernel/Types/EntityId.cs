using System;

namespace Inflow.Shared.Abstractions.Kernel.Types;

public class EntityId : TypeId
{
    public EntityId(Guid value) : base(value)
    {
    }

    public static implicit operator EntityId(Guid id) => new(id);

    public static implicit operator Guid(EntityId id) => id.Value;
}