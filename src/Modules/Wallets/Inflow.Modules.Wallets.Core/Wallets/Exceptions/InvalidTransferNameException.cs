using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Wallets.Core.Wallets.Exceptions
{
    internal class InvalidTransferNameException : InflowException
    {
        public string Name { get; }

        public InvalidTransferNameException(string name) : base($"Transfer name: '{name}' is invalid.")
        {
            Name = name;
        }
    }
}