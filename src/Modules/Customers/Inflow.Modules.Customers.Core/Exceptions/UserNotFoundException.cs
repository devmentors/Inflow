using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class UserNotFoundException : InflowException
    {
        public string Email { get; }
        
        public UserNotFoundException(string email) : base($"User with email: '{email}' was not found.")
        {
            Email = email;
        }
    }
}