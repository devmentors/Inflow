using System;

namespace Inflow.Modules.Wallets.Application.Wallets.DTO
{
    internal class TransferDto
    {
        public string Type { get; set; }
        public Guid TransferId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}