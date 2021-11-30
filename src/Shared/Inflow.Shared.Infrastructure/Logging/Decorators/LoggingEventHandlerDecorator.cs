using System;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Inflow.Shared.Abstractions.Contexts;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IMessageContextProvider _messageContextProvider;
    private readonly IContext _context;
    private readonly ILogger<LoggingEventHandlerDecorator<T>> _logger;

    public LoggingEventHandlerDecorator(IEventHandler<T> handler, IMessageContextProvider messageContextProvider,
        IContext context, ILogger<LoggingEventHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _messageContextProvider = messageContextProvider;
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var module = @event.GetModuleName();
        var name = @event.GetType().Name.Underscore();
        var messageContext = _messageContextProvider.Get(@event);
        var requestId = _context.RequestId;
        var traceId = _context.TraceId;
        var userId = _context.Identity?.Id;
        var messageId = messageContext?.MessageId;
        var correlationId = messageContext?.Context.CorrelationId ?? _context.CorrelationId;
        _logger.LogInformation("Handling an event: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]...",
            name, module, requestId, messageId, correlationId, traceId, userId);
        await _handler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation("Handled an event: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]",
            name, module, requestId, messageId, correlationId, traceId, userId);
    }
}