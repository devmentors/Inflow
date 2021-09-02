using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Shared.Abstractions.Kernel.Exceptions
{
    public class InvalidAmountException : InflowException
    {
        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
        {
            Amount = amount;
        }
    }
}