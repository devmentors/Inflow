using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Modules.Wallets.Application.Wallets.Storage;
using Inflow.Shared.Abstractions.Contexts;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries.Handlers;

internal sealed class GetWalletHandler : IQueryHandler<GetWallet, WalletDetailsDto>
{
    private readonly IWalletStorage _storage;
    private readonly IContext _context;

    public GetWalletHandler(IWalletStorage storage, IContext context)
    {
        _storage = storage;
        _context = context;
    }

    public async Task<WalletDetailsDto> HandleAsync(GetWallet query, CancellationToken cancellationToken = default)
    {
        // Owner cannot access the other wallets
        var wallet = await _storage.FindAsync(x => x.Id == query.WalletId);
        if (wallet is null || _context.Identity.IsUser() && _context.Identity.Id != wallet.OwnerId)
        {
            return null;
        }
            
        return wallet.AsDetailsDto();
    }
}