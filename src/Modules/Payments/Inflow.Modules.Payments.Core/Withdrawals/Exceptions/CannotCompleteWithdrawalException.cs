using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions
{
    internal class CannotCompleteWithdrawalException : InflowException
    {
        public Guid DepositId { get; }

        public CannotCompleteWithdrawalException(Guid depositId)
            : base($"Withdrawal with ID: '{depositId}' cannot be completed.")
        {
            DepositId = depositId;
        }
    }
}