using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Modules.Wallets.Application.Wallets.Storage;
using Inflow.Modules.Wallets.Core.Wallets.Entities;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Application.Wallets.Queries.Handlers;

internal sealed class BrowseWalletsHandler : IQueryHandler<BrowseWallets, Paged<WalletDto>>
{
    private readonly IWalletStorage _storage;

    public BrowseWalletsHandler(IWalletStorage storage)
    {
        _storage = storage;
    }

    public async Task<Paged<WalletDto>> HandleAsync(BrowseWallets query,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<Wallet, bool>> expression = x => true;

        if (!string.IsNullOrWhiteSpace(query.Currency))
        {
            _ = new Currency(query.Currency);
            expression = expression.And(x => x.Currency == query.Currency);
        }

        if (query.OwnerId.HasValue)
        {
            expression = expression.And(x => x.OwnerId == query.OwnerId);
        }

        var result = await _storage.BrowseAsync(expression, query);
        var wallets = result.Items.Select(x => x.AsDto()).ToList();

        return Paged<WalletDto>.From(result, wallets);
    }
}