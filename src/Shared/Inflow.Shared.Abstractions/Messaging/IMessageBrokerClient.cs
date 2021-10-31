using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Messaging
{
    public interface IMessageBrokerClient
    {
        Task SendAsync(IMessage message, Guid messageId, CancellationToken cancellationToken = default);
    }
}