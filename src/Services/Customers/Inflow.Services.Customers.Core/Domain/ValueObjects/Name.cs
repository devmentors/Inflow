using System;
using Inflow.Services.Customers.Core.Exceptions;

namespace Inflow.Services.Customers.Core.Domain.ValueObjects;

public class Name : IEquatable<Name>
{
    public string Value { get; }
        
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 50 or < 3)
        {
            throw new InvalidNameException(value);
        }

        Value = value.Trim().ToLowerInvariant().Replace(" ", ".");
    }

    public static implicit operator Name(string value) => value is null ? null : new Name(value);

    public static implicit operator string(Name value) => value?.Value;

    public bool Equals(Name other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Name)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
        
    public override string ToString() => Value;
}