using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class InvalidNameException : InflowException
    {
        public string Name { get; }

        public InvalidNameException(string name) : base($"Name: '{name}' is invalid.")
        {
            Name = name;
        }
    }
}