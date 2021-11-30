using System;
using Inflow.Modules.Wallets.Core.Owners.Exceptions;

namespace Inflow.Modules.Wallets.Core.Owners.ValueObjects;

internal class TaxId : IEquatable<TaxId>
{
    public string Value { get; }
        
    public TaxId(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 20)
        {
            throw new InvalidTaxIdException(value);
        }

        Value = value.Trim();
    }

    public static implicit operator TaxId(string value) => value is null ? null : new TaxId(value);

    public static implicit operator string(TaxId value) => value?.Value;

    public bool Equals(TaxId other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TaxId)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
        
    public override string ToString() => Value;
}