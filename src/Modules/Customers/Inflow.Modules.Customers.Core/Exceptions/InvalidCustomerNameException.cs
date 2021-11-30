using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions;

internal class InvalidCustomerNameException : InflowException
{
    public Guid CustomerId { get; }

    public InvalidCustomerNameException(Guid customerId)
        : base($"Customer with ID: '{customerId}' has invalid name.")
    {
        CustomerId = customerId;
    }
}