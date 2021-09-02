using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Shared.Infrastructure.Events
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}