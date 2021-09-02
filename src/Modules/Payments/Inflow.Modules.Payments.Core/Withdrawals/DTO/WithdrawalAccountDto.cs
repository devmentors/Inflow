using System;

namespace Inflow.Modules.Payments.Core.Withdrawals.DTO
{
    internal class WithdrawalAccountDto
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public string Iban { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}