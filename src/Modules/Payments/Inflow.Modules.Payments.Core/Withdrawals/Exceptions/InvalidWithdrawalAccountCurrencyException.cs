using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Core.Withdrawals.Exceptions;

public class InvalidWithdrawalAccountCurrencyException : InflowException
{
    public Guid AccountId { get; }
    public string Currency { get; }

    public InvalidWithdrawalAccountCurrencyException(Guid accountId, string currency)
        : base($"Withdrawal account with ID: '{accountId}' has invalid currency: '{currency}'.")
    {
        AccountId = accountId;
        Currency = currency;
    }
}