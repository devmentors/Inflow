using System;

namespace Inflow.Shared.Abstractions.Messaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageAttribute : Attribute
    {
        public string Module { get; }
        public bool Enabled { get; }

        public MessageAttribute(string module = null, bool enabled = true)
        {
            Module = module ?? string.Empty;
            Enabled = enabled;
        }
    }
}