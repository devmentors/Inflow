using Convey.MessageBrokers;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Inflow.Shared.Infrastructure.Messaging.RabbitMQ
{
    internal sealed class RabbitMqMessageSubscriber : IMessageSubscriber
    {
        private readonly IBusSubscriber _busSubscriber;

        public RabbitMqMessageSubscriber(IBusSubscriber busSubscriber)
        {
            _busSubscriber = busSubscriber;
        }

        public IMessageSubscriber SubscribeCommand<T>() where T : class, ICommand
        {
            _busSubscriber.Subscribe<T>(async (serviceProvider, command, _) =>
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
            });

            return this;
        }

        public IMessageSubscriber SubscribeEvent<T>() where T : class, IEvent
        {
            _busSubscriber.Subscribe<T>(async (serviceProvider, @event, _) =>
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IEventHandler<T>>().HandleAsync(@event);
            });

            return this;
        }
    }
}