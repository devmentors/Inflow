using System;
using System.Threading;
using System.Threading.Tasks;
using Convey.MessageBrokers;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Shared.Infrastructure.Messaging.RabbitMQ
{
    internal sealed class RabbitMqMessageBrokerClient : IMessageBrokerClient
    {
        private readonly IBusPublisher _publisher;

        public RabbitMqMessageBrokerClient(IBusPublisher publisher)
        {
            _publisher = publisher;
        }

        public Task SendAsync(IMessage message, Guid messageId, CancellationToken cancellationToken = default)
            => _publisher.PublishAsync(message, messageId.ToString("N"));
    }
}