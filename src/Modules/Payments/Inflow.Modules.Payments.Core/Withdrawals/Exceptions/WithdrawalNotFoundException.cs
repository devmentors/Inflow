using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions;

public class WithdrawalNotFoundException : InflowException
{
    public Guid DepositId { get; }

    public WithdrawalNotFoundException(Guid depositId) : base($"Withdrawal with ID: '{depositId}' was not found.")
    {
        DepositId = depositId;
    }
}