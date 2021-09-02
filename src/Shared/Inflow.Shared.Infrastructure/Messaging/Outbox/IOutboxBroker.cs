using System.Threading.Tasks;
using Inflow.Shared.Abstractions.Messaging;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox
{
    public interface IOutboxBroker
    {
        bool Enabled { get; }
        Task SendAsync(params IMessage[] messages);
    }
}