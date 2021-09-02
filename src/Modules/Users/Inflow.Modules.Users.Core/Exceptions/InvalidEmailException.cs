using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions
{
    internal class InvalidEmailException : InflowException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"State is invalid: '{email}'.")
        {
            Email = email;
        }
    }
}