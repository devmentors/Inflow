using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions;

internal class EmailInUseException : InflowException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}