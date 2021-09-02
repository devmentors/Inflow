using System;
using Inflow.Modules.Wallets.Core.Wallets.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.ValueObjects
{
    internal class TransferMetadata : IEquatable<TransferMetadata>
    {
        public string Value { get; }
        
        public TransferMetadata(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            
            if (value.Length > 1000)
            {
                throw new InvalidTransferMetadataException(value);
            }

            Value = value.Trim();
        }

        public static implicit operator TransferMetadata(string value) => value is null ? null : new TransferMetadata(value);

        public static implicit operator string(TransferMetadata value) => value?.Value;

        public bool Equals(TransferMetadata other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TransferMetadata)obj);
        }

        public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
        
        public override string ToString() => Value;
    }
}