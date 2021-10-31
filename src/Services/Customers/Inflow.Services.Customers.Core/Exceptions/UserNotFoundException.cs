using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class UserNotFoundException : InflowException
    {
        public string Email { get; }
        
        public UserNotFoundException(string email) : base($"User with email: '{email}' was not found.")
        {
            Email = email;
        }
    }
}