using System;
using Inflow.Modules.Wallets.Core.Owners.Exceptions;

namespace Inflow.Modules.Wallets.Core.Owners.ValueObjects;

internal class OwnerName : IEquatable<OwnerName>
{
    public string Value { get; }
        
    public OwnerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new InvalidOwnerNameException(value);
        }

        Value = value.Trim().ToLowerInvariant().Replace(" ", ".");
    }

    public static implicit operator OwnerName(string value) => value is null ? null : new OwnerName(value);

    public static implicit operator string(OwnerName value) => value?.Value;

    public bool Equals(OwnerName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((OwnerName)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
        
    public override string ToString() => Value;
}