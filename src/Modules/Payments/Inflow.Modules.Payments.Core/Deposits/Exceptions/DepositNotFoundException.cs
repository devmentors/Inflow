using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Deposits.Exceptions
{
    public class DepositNotFoundException : InflowException
    {
        public Guid DepositId { get; }

        public DepositNotFoundException(Guid depositId) : base($"Deposit with ID: '{depositId}' was not found.")
        {
            DepositId = depositId;
        }
    }
}