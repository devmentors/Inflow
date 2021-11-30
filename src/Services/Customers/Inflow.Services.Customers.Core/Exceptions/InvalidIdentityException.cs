using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions;

public class InvalidIdentityException : InflowException
{
    public string Type { get; }

    public InvalidIdentityException(string type, string series)
        : base($"Identity type: '{type}', series: '{series}' is invalid.")
    {
        Type = type;
    }
}