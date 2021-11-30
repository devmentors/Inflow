using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Deposits.Exceptions;

public class DepositAccountUnverifiedException : InflowException
{
    public Guid AccountId { get; }
    public Guid CustomerId { get; }

    public DepositAccountUnverifiedException(Guid accountId, Guid customerId)
        : base($"Deposit account with ID: '{accountId}' for customer with ID: '{customerId}' is unverified.")
    {
        AccountId = accountId;
        CustomerId = customerId;
    }
}