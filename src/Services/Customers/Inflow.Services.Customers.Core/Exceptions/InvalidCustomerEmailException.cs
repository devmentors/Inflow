using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class InvalidCustomerEmailException : InflowException
    {
        public Guid CustomerId { get; }

        public InvalidCustomerEmailException(Guid customerId)
            : base($"Customer with ID: '{customerId}' has invalid email.")
        {
            CustomerId = customerId;
        }
    }
}