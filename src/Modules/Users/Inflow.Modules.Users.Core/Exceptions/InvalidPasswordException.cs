using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions;

internal class InvalidPasswordException : InflowException
{
    public string Reason { get; }

    public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
    {
        Reason = reason;
    }
}