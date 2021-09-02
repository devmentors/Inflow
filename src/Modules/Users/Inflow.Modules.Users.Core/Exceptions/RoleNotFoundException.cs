using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions
{
    internal class RoleNotFoundException : InflowException
    {
        public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
        {
        }
    }
}