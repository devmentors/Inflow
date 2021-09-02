using System.Threading;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Kernel
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default);
        Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default);
    }
}