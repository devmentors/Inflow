using System;

namespace Inflow.Shared.Abstractions.Messaging;

[AttributeUsage(AttributeTargets.Class)]
public class ExternalMessageAttribute : Attribute
{
    public string Topic { get; }
    public string Key { get; }
    public string Queue { get; }

    public ExternalMessageAttribute(string topic = null, string key = null, string queue = null)
    {
        Topic = topic;
        Key = key;
        Queue = queue;
    }
}