using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions
{
    internal class UserNotActiveException : InflowException
    {
        public Guid UserId { get; }

        public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
        {
            UserId = userId;
        }
    }
}