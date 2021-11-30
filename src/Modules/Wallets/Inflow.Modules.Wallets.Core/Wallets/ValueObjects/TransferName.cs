using System;
using Inflow.Modules.Wallets.Core.Wallets.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.ValueObjects;

internal class TransferName : IEquatable<TransferName>
{
    public string Value { get; }
        
    public TransferName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }
            
        if (value.Length > 100)
        {
            throw new InvalidTransferNameException(value);
        }

        Value = value.Trim().Replace(" ", "_");
    }

    public static implicit operator TransferName(string value) => value is null ? null : new TransferName(value);

    public static implicit operator string(TransferName value) => value?.Value;

    public bool Equals(TransferName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TransferName)obj);
    }

    public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
        
    public override string ToString() => Value;
}