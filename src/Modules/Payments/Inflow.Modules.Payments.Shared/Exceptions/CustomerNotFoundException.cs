using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Infrastructure.Exceptions
{
    public class CustomerNotFoundException : InflowException
    {
        public Guid CustomerId { get; }

        public CustomerNotFoundException(Guid customerId)
            : base($"Customer with ID: '{customerId}' was not found.")
        {
            CustomerId = customerId;
        }
    }
}