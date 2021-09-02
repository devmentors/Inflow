using System.Threading;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    }
}