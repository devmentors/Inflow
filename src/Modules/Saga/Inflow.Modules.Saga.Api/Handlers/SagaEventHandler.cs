using System.Threading;
using System.Threading.Tasks;
using Chronicle;
using Inflow.Modules.Saga.Api.Messages;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Saga.Api.Handlers;

internal sealed class SagaEventHandler :
    IEventHandler<CustomerVerified>,
    IEventHandler<WalletAdded>,
    IEventHandler<DepositCompleted>,
    IEventHandler<FundsAdded>
{
    private readonly ISagaCoordinator _sagaCoordinator;

    public SagaEventHandler(ISagaCoordinator sagaCoordinator)
    {
        _sagaCoordinator = sagaCoordinator;
    }

    public Task HandleAsync(CustomerVerified @event, CancellationToken cancellationToken = default)
        => HandleAsync(@event);

    public Task HandleAsync(WalletAdded @event, CancellationToken cancellationToken = default)
        => HandleAsync(@event);

    public Task HandleAsync(DepositCompleted @event, CancellationToken cancellationToken = default)
        => HandleAsync(@event);

    public Task HandleAsync(FundsAdded @event, CancellationToken cancellationToken = default)
        => HandleAsync(@event);

    private Task HandleAsync<T>(T message) where T : class
        => _sagaCoordinator.ProcessAsync(message, SagaContext.Empty);
}