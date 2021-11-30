using System;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries;

internal class GetWallet : IQuery<WalletDetailsDto>
{
    public Guid WalletId { get; set; }
}