using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Shared.Abstractions.Kernel.Exceptions
{
    public class UnsupportedNationalityException : InflowException
    {
        public string Nationality { get; }

        public UnsupportedNationalityException(string nationality) : base($"Nationality: '{nationality}' is unsupported.")
        {
            Nationality = nationality;
        }
    }
}