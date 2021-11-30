using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Owners.Exceptions;

internal class InvalidTaxIdException : InflowException
{
    public string TaxId { get; }

    public InvalidTaxIdException(string taxId) : base($"Tax ID: '{taxId}' is invalid.")
    {
        TaxId = taxId;
    }
}