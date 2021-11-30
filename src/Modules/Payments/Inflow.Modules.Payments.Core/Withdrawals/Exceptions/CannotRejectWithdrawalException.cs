using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions;

internal class CannotRejectWithdrawalException : InflowException
{
    public Guid DepositId { get; }

    public CannotRejectWithdrawalException(Guid depositId)
        : base($"Withdrawal with ID: '{depositId}' cannot be rejected.")
    {
        DepositId = depositId;
    }
}