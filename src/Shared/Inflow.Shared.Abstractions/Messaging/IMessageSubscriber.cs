using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Shared.Abstractions.Messaging
{
    public interface IMessageSubscriber
    {
        IMessageSubscriber SubscribeCommand<T>() where T : class, ICommand;
        IMessageSubscriber SubscribeEvent<T>() where T : class, IEvent;
    }
}