using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries;

internal class BrowseTransfers : PagedQuery<TransferDto>
{
    public string Currency { get; set; }
    public string Name { get; set; }
}