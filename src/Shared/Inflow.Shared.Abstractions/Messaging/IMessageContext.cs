using System;
using Inflow.Shared.Abstractions.Contexts;

namespace Inflow.Shared.Abstractions.Messaging
{
    public interface IMessageContext
    {
        public Guid MessageId { get; }
        public IContext Context { get; }
    }
}