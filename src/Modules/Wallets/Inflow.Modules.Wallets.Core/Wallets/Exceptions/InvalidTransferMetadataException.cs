using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions
{
    internal class InvalidTransferMetadataException : InflowException
    {
        public string Metadata { get; }

        public InvalidTransferMetadataException(string metadata) : base($"Transfer metadata: '{metadata}' is invalid.")
        {
            Metadata = metadata;
        }
    }
}