using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions;

public class WithdrawalAccountUnverifiedException : InflowException
{
    public Guid AccountId { get; }
    public Guid CustomerId { get; }

    public WithdrawalAccountUnverifiedException(Guid accountId, Guid customerId)
        : base($"Withdrawal account with ID: '{accountId}' for customer with ID: '{customerId}' is unverified.")
    {
        AccountId = accountId;
        CustomerId = customerId;
    }
}