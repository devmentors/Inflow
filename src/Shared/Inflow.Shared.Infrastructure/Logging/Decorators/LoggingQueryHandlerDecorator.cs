using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Inflow.Shared.Abstractions.Contexts;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _handler;
    private readonly IContext _context;
    private readonly ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> _logger;

    public LoggingQueryHandlerDecorator(IQueryHandler<TQuery, TResult> handler,
        IContext context, ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> logger)
    {
        _handler = handler;
        _context = context;
        _logger = logger;
    }

    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        var module = query.GetModuleName();
        var name = query.GetType().Name.Underscore();
        var requestId = _context.RequestId;
        var correlationId = _context.CorrelationId;
        var traceId = _context.TraceId;
        var userId = _context.Identity?.Id;
        _logger.LogInformation("Handling a query: {Name} ({Module}) [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]...",
            name, module, requestId, correlationId, traceId, userId);
        var result = await _handler.HandleAsync(query, cancellationToken);
        _logger.LogInformation("Handled a query: {Name} ({Module}) [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]",
            name, module, requestId, correlationId, traceId, userId);

        return result;
    }
}