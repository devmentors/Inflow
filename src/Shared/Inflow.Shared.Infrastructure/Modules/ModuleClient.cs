using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Shared.Infrastructure.Messaging.Contexts;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Modules;

namespace Inflow.Shared.Infrastructure.Modules;

public sealed class ModuleClient : IModuleClient
{
    private readonly ConcurrentDictionary<Type, MessageAttribute> _messages = new();
    private readonly IModuleRegistry _moduleRegistry;
    private readonly IModuleSerializer _moduleSerializer;
    private readonly IMessageContextRegistry _messageContextRegistry;
    private readonly IMessageContextProvider _messageContextProvider;

    public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer,
        IMessageContextRegistry messageContextRegistry, IMessageContextProvider messageContextProvider)
    {
        _moduleRegistry = moduleRegistry;
        _moduleSerializer = moduleSerializer;
        _messageContextRegistry = messageContextRegistry;
        _messageContextProvider = messageContextProvider;
    }

    public Task SendAsync(string path, object request, CancellationToken cancellationToken = default)
        => SendAsync<object>(path, request, cancellationToken);

    public async Task<TResult> SendAsync<TResult>(string path, object request,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var registration = _moduleRegistry.GetRequestRegistration(path);
        if (registration is null)
        {
            throw new InvalidOperationException($"No action has been defined for path: '{path}'.");
        }

        var receiverRequest = TranslateType(request, registration.RequestType);
        var result = await registration.Action(receiverRequest, cancellationToken);

        return result is null ? null : TranslateType<TResult>(result);
    }

    public async Task PublishAsync(object message, CancellationToken cancellationToken = default)
    {
        var module = message.GetModuleName();
        var key = message.GetType().Name;
        var registrations = _moduleRegistry
            .GetBroadcastRegistrations(key)
            .Where(r => r.ReceiverType != message.GetType());

        var tasks = new List<Task>();

        foreach (var registration in registrations)
        {
            if (!_messages.TryGetValue(registration.ReceiverType, out var messageAttribute))
            {
                messageAttribute = registration.ReceiverType.GetCustomAttribute<MessageAttribute>();
                if (message is ICommand)
                {
                    messageAttribute = message.GetType().GetCustomAttribute<MessageAttribute>();
                    module = registration.ReceiverType.GetModuleName();
                }

                if (messageAttribute is not null)
                {
                    _messages.TryAdd(registration.ReceiverType, messageAttribute);
                }
            }

            if (messageAttribute is not null && !string.IsNullOrWhiteSpace(messageAttribute.Module) &&
                (!messageAttribute.Enabled || messageAttribute.Module != module))
            {
                continue;
            }

            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            if (message is IMessage messageData)
            {
                var messageContext = _messageContextProvider.Get(messageData);
                _messageContextRegistry.Set((IMessage)receiverMessage, messageContext);
            }

            tasks.Add(action(receiverMessage, cancellationToken));
        }

        await Task.WhenAll(tasks);
    }

    private T TranslateType<T>(object value)
        => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));

    private object TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
}