using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Services.Customers.Core.Exceptions
{
    public class CannotVerifyCustomerException : InflowException
    {
        public Guid CustomerId { get; }

        public CannotVerifyCustomerException(Guid customerId)
            : base($"Customer with ID: '{customerId}' cannot be verified.")
        {
            CustomerId = customerId;
        }
    }
}