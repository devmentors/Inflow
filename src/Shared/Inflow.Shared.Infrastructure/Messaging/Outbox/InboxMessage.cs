using System;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox;

public class InboxMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}