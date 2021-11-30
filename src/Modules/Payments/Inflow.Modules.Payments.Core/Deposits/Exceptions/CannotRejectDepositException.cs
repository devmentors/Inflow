using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Deposits.Exceptions;

internal class CannotRejectDepositException : InflowException
{
    public Guid DepositId { get; }

    public CannotRejectDepositException(Guid depositId)
        : base($"Deposit with ID: '{depositId}' cannot be rejected.")
    {
        DepositId = depositId;
    }
}