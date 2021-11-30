using System;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries;

internal class GetTransfer : IQuery<TransferDetailsDto>
{
    public Guid? TransferId { get; set; }
}