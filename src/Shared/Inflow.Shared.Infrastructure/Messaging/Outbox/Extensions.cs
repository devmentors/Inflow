using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox
{
    public static class Extensions
    {
        public static IServiceCollection AddOutbox<T>(this IServiceCollection services) where T : DbContext
        {
            var outboxOptions = services.GetOptions<OutboxOptions>("outbox");
            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.AddTransient<IInbox, EfInbox<T>>();
            services.AddTransient<IOutbox, EfOutbox<T>>();
            services.AddTransient<EfInbox<T>>();
            services.AddTransient<EfOutbox<T>>();
            
            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<InboxTypeRegistry>().Register<EfInbox<T>>();
            serviceProvider.GetRequiredService<OutboxTypeRegistry>().Register<EfOutbox<T>>();

            return services;
        }

        public static IServiceCollection AddOutbox(this IServiceCollection services)
        {
            var outboxOptions = services.GetOptions<OutboxOptions>("outbox");
            services.AddSingleton(outboxOptions);
            services.AddSingleton(new InboxTypeRegistry());
            services.AddSingleton(new OutboxTypeRegistry());
            services.AddSingleton<IOutboxBroker, OutboxBroker>();
            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.TryDecorate(typeof(IEventHandler<>), typeof(InboxEventHandlerDecorator<>));
            services.AddHostedService<OutboxProcessor>();
            services.AddHostedService<InboxCleanupProcessor>();
            services.AddHostedService<OutboxCleanupProcessor>();

            return services;
        }
    }
}