using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : InflowException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}