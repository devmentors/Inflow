using System;

namespace Inflow.Modules.Payments.Core.Withdrawals.DTO
{
    internal class WithdrawalDto
    {
        public Guid WithdrawalId { get; set; }
        public string Status { get; set; }
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}