using Convey;
using Convey.MessageBrokers.RabbitMQ;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Inflow.Shared.Infrastructure.Messaging.RabbitMQ;

public static class Extensions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        services
            .AddConvey()
            .AddRabbitMq();
            
        services.AddHostedService<CustomRabbitMqInitializer>();
        services.AddSingleton<IMessageBrokerClient, RabbitMqMessageBrokerClient>();
        services.AddSingleton<IMessageSubscriber, RabbitMqMessageSubscriber>();
        services.AddSingleton<IConventionsBuilder, CustomConventionsBuilder>();

        return services;
    }
}