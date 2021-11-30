using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Payments.Infrastructure.Exceptions;

internal class InvalidIbanException : InflowException
{
    public string Iban { get; }

    public InvalidIbanException(string iban) : base($"IBAN: '{iban}' is invalid.")
    {
        Iban = iban;
    }
}