using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class InvalidAddressException : InflowException
    {
        public string Address { get; }

        public InvalidAddressException(string address) : base($"Address: '{address}' is invalid.")
        {
            Address = address;
        }
    }
}