using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class CustomerNotActiveException : InflowException
    {
        public Guid CustomerId { get; }

        public CustomerNotActiveException(Guid customerId)
            : base($"Customer with ID: '{customerId}' is not active.")
        {
            CustomerId = customerId;
        }
    }
}