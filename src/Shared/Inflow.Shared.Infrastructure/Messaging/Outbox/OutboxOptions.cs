using System;

namespace Inflow.Shared.Infrastructure.Messaging.Outbox
{
    public class OutboxOptions
    {
        public bool Enabled { get; set; }
        public TimeSpan? StartDelay { get; set; }
        public TimeSpan? Interval { get; set; }
        public TimeSpan? InboxCleanupInterval { get; set; }
        public TimeSpan? OutboxCleanupInterval { get; set; }
        public bool TransactionsDisabled { get; set; }
    }
}