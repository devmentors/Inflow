using System;

namespace Inflow.Shared.Abstractions.Exceptions
{
    public abstract class InflowException : Exception
    {
        protected InflowException(string message) : base(message)
        {
        }
    }
}