using System;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string TraceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
    }
}