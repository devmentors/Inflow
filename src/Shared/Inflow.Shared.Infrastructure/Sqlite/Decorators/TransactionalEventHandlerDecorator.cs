using System;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Shared.Infrastructure.Sqlite.Decorators;

[Decorator]
public class TransactionalEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeTypeRegistry;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TransactionalEventHandlerDecorator<T>> _logger;

    public TransactionalEventHandlerDecorator(IEventHandler<T> handler, UnitOfWorkTypeRegistry unitOfWorkTypeTypeRegistry,
        IServiceProvider serviceProvider, ILogger<TransactionalEventHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _unitOfWorkTypeTypeRegistry = unitOfWorkTypeTypeRegistry;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var unitOfWorkType = _unitOfWorkTypeTypeRegistry.Resolve<T>();
        if (unitOfWorkType is null)
        {
            await _handler.HandleAsync(@event, cancellationToken);
            return;
        }

        var unitOfWork = (IUnitOfWork) _serviceProvider.GetRequiredService(unitOfWorkType);
        var unitOfWorkName = unitOfWorkType.Name;
        var name = @event.GetType().Name.Underscore();
        _logger.LogInformation("Handling: {Name} using TX ({UnitOfWorkName})...", name, unitOfWorkName);
        await unitOfWork.ExecuteAsync(() => _handler.HandleAsync(@event, cancellationToken));
        _logger.LogInformation("Handled: {Name} using TX ({UnitOfWorkName})", name, unitOfWorkName);
    }
}