using System;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries
{
    internal class BrowseWallets : PagedQuery<WalletDto>
    {
        public Guid? OwnerId { get; set; }
        public string Currency { get; set; }
    }
}