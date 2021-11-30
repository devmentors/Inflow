using System;
using System.Collections.Generic;
using Inflow.Modules.Customers.Core.Exceptions;

namespace Inflow.Modules.Customers.Core.Domain.ValueObjects;

internal class Identity : IEquatable<Identity>
{
    private static readonly HashSet<string> AllowedTypes = new()
    {
        "passport", "id_card", "drivers_license"
    };
        
    public string Type { get; }
    public string Series { get; }

    public Identity(string type, string series)
    {
        if (string.IsNullOrWhiteSpace(type) || series.Length > 20)
        {
            throw new InvalidIdentityException(type, series);
        }

        type = type.ToLowerInvariant();
        if (!AllowedTypes.Contains(type))
        {
            throw new InvalidIdentityException(type, series);
        }
            
        if (string.IsNullOrWhiteSpace(series) || series.Length > 20)
        {
            throw new InvalidIdentityException(type, series);
        }
            
        Type = type;
        Series = series;
    }
        
    public static implicit operator string(Identity value) => $"{value.Type},{value.Series}";

    public static implicit operator Identity(string value) => From(value);

    public override string ToString() => $"{Type},{Series}";

    public static Identity From(string value) 
    {
        var (type, series) = Split(value);

        return new Identity(type, series);
    }

    private static (string type, string series) Split(string value)
    {
        var values = value.Split(",");
        return (values[0], values[1]);
    }

    public bool Equals(Identity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Series == other.Series;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Identity)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Type, Series);
}