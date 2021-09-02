using System.Collections.Generic;

namespace Inflow.Modules.Wallets.Application.Wallets.DTO
{
    internal class WalletDetailsDto : WalletDto
    {
        public decimal Amount { get; set; }
        public List<TransferDto> Transfers { get; set; }
    }
}