using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Users.Core.Exceptions;

internal class SignUpDisabledException : InflowException
{
    public SignUpDisabledException() : base("Sign up is disabled.")
    {
    }
}