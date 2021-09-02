using System;
using System.Collections.Generic;

namespace Inflow.Shared.Abstractions.Kernel.Types
{
    public class AggregateId<T> : IEquatable<AggregateId<T>>
    {
        public T Value { get; }

        public AggregateId(T value)
        {
            Value = value;
        }

        public bool Equals(AggregateId<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AggregateId<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }

    public class AggregateId : AggregateId<Guid>
    {
        public AggregateId() : this(Guid.NewGuid())
        {
        }
        
        public AggregateId(Guid value) : base(value)
        {
        }

        public static implicit operator Guid(AggregateId id) => id.Value;
        public static implicit operator AggregateId(Guid id) => new(id);
        
        public override string ToString() => Value.ToString();
    }
}