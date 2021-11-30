using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions;

internal class CustomerLockedException : InflowException
{
    public Guid CustomerId { get; }

    public CustomerLockedException(Guid customerId)
        : base($"Customer with ID: '{customerId}' is locked.")
    {
        CustomerId = customerId;
    }
}