using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions
{
    public class WithdrawalAccountAlreadyExistsException : InflowException
    {
        public Guid CustomerId { get; }
        public string Currency { get; }

        public WithdrawalAccountAlreadyExistsException(Guid customerId, string currency)
            : base($"Withdrawal account for customer with ID: '{customerId}', currency: '{currency}' already exists.")
        {
            CustomerId = customerId;
            Currency = currency;
        }
    }
}