using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions
{
    public class InvalidTransferAmountException : InflowException
    {
        public decimal Amount { get; }

        public InvalidTransferAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
        {
            Amount = amount;
        }
    }
}