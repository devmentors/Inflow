using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions
{
    internal class UserNotFoundException : InflowException
    {
        public string Email { get; }
        public Guid UserId { get; }

        public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId;
        }
        
        public UserNotFoundException(string email) : base($"User with email: '{email}' was not found.")
        {
            Email = email;
        }
    }
}