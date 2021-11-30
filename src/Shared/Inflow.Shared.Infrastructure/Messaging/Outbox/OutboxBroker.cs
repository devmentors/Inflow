using System;
using System.Threading.Tasks;
using Inflow.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox;

internal sealed class OutboxBroker : IOutboxBroker
{
    private readonly IServiceProvider _serviceProvider;
    private readonly OutboxTypeRegistry _registry;

    public OutboxBroker(IServiceProvider serviceProvider, OutboxTypeRegistry registry, OutboxOptions options)
    {
        _serviceProvider = serviceProvider;
        _registry = registry;
        Enabled = options.Enabled;
    }

    public bool Enabled { get; }

    public async Task SendAsync(params IMessage[] messages)
    {
        var message = messages[0]; // Not possible to send messages from different modules at once
        var outboxType = _registry.Resolve(message);
        if (outboxType is null)
        {
            throw new InvalidOperationException($"Outbox is not registered for module: '{message.GetModuleName()}'.");
        }

        using var scope = _serviceProvider.CreateScope();
        var outbox = (IOutbox)scope.ServiceProvider.GetRequiredService(outboxType);
        await outbox.SaveAsync(messages);
    }
}