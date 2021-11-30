using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Deposits.Exceptions;

public class DepositAccountAlreadyExistsException : InflowException
{
    public Guid CustomerId { get; }
    public string Currency { get; }

    public DepositAccountAlreadyExistsException(Guid customerId, string currency)
        : base($"Deposit account for customer with ID: '{customerId}', currency: '{currency}' already exists.")
    {
        CustomerId = customerId;
        Currency = currency;
    }
}