using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules;

internal sealed class ModuleBroadcastRegistration
{
    public Type ReceiverType { get; }
    public Func<object, CancellationToken, Task> Action { get; }
    public string Key => ReceiverType.Name;

    public ModuleBroadcastRegistration(Type receiverType, Func<object, CancellationToken, Task> action)
    {
        ReceiverType = receiverType;
        Action = action;
    }
}